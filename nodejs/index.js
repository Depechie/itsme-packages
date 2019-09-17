var path = require('path');
var ffi = require('ffi');
var ref = require('ref');
var struct = require('ref-struct');

var Response = struct({
  data: ref.types.CString,
  error: ref.types.CString
});

function ErrorMessage(message) {
    const error = JSON.parse(message);
    return {
        message: error.message || 'Could not parse error object.'
    }
}

function createUser(data){
    const user = JSON.parse(data);
    return {
        sub: user.sub,
        aud: user.aud,
        birthdate: user.birthdate,
        gender: user.gender,
        name: user.name,
        iss: user.iss,
        phoneNumberVerified: user.phoneNumberVerified,
        phoneNumber: user.phoneNumber,
        givenName: user.givenName,
        familyName: user.familyName,
        locale: user.locale,
        email: user.email,
        address: 'parsedAddress' in user ? createAddress(user.parsedAddress) : undefined
    }
}

function createAddress(address) {
    return {
        country: address.country,
        streetAddress: address.streetAddress,
        locality: address.locality,
        postalCode: address.postalCode,
    }
}

function createItsmeSettings(clientId, redirectUri, privateJwkSet, environment){
    return {
        client_id: clientId,
        redirect_uri: redirectUri,
        private_jwk_set: privateJwkSet,
        app_environment: environment,
    }
}

function createUrlConfiguration(scopes, serviceCode, redirectUri) {
    return {
        scopes: scopes,
        serviceCode: serviceCode,
        redirectUri: redirectUri,
    }
}

function getItsmeBin(){
    const platformExtentionMap = {
        aix: 'so',
        win32: 'dll',
        darwin: 'dylib',
    };
    const extension = platformExtentionMap[process.platform];
    return path.resolve(__dirname+'/libs/itsme_lib.'+extension);
}

class Client {
    constructor(settings){
        let itsmeBin = getItsmeBin();
        this.itsmeLib = ffi.Library(itsmeBin, {
            'Init': [ref.types.CString, [ref.types.CString]],
            'GetUserDetails': [Response, [ref.types.CString]],
            'GetAuthenticationURL': [Response, [ref.types.CString]]
        });
        const response = this.itsmeLib.Init(JSON.stringify(settings))
        if (response){
            const error = ErrorMessage(response);
            throw new Error(error.message);
        }
    }

    getUserDetails(authorizationCode) {
        const response = this.itsmeLib.GetUserDetails(authorizationCode);
        if (response.error) {
            const error = ErrorMessage(response.error);
            throw Error(error.message);
        }
        return createUser(response.data);
    }

    getAuthenticationUrl(urlConfiguration) {
        var urlconfig = urlConfiguration;
        var response = this.itsmeLib.GetAuthenticationURL(JSON.stringify(urlconfig));
        if (response.error) {
            var error = ErrorMessage(response.error);
            throw new Error(error.message);
        }
        return createUrlConfiguration(response.data);
    }
}

module.exports.Client = Client;
module.exports.createItsmeSettings = createItsmeSettings;
module.exports.createUrlConfiguration = createUrlConfiguration;
