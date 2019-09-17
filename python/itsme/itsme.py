from json import loads, dumps
import typing
from typing import Dict, List, Tuple
from ctypes import CDLL, c_char_p, Structure
from os.path import abspath, dirname
from os import name as operating_system


class Response(Structure):
    _fields_ = [('data', c_char_p), ('error', c_char_p)]


class Error(object):
    def __init__(self, json):
        error = loads(json)
        self.message = (
            error['message'] if 'message' in error else 'Could not parse error object'
        )


class User(object):
    def __init__(self, json):
        user = loads(json)
        self.sub = user['sub'] if 'sub' in user else ''
        self.aud = user['aud'] if 'aud' in user else ''
        self.birthdate = user['birthdate'] if 'birthdate' in user else ''
        self.gender = user['gender'] if 'gender' in user else ''
        self.name = user['name'] if 'name' in user else ''
        self.iss = user['iss'] if 'iss' in user else ''
        self.phone_number_verified = user['phone_number_verified'] if 'phone_number_verified' in user else ''
        self.phone_number = user['phone_number'] if 'phone_number' in user else ''
        self.given_name = user['given_name'] if 'given_name' in user else ''
        self.family_name = user['family_name'] if 'family_name' in user else ''
        self.locale = user['locale'] if 'locale' in user else ''
        self.email = user['email'] if 'email' in user else ''
        if 'parsed_address' in user:
            self.address = Address(user['parsed_address'])


class Address(object):
    def __init__(self, json):
        address = loads(json)
        self.country = address['country'] if 'country' in address else ''
        self.street_address = address['street_address'] if 'street_address' in address else ''
        self.locality = address['locality'] if 'locality' in address else ''
        self.postal_code = address['postal_code'] if 'postal_code' in address else ''


class ItsmeSettings(object):
    def __init__(self, client_id: str, redirect_uri: str, private_jwk_set: str, app_environment: str):
        self.client_id = client_id
        self.redirect_uri = redirect_uri
        self.private_jwk_set = private_jwk_set
        self.app_environment = app_environment


class UrlConfiguration(object):
    def __init__(self, scopes: List[str], service_code: str, request_uri: str):
        self.scopes = scopes
        self.request_uri = request_uri
        self.service_code = service_code


itsme_lib = None


class Client(object):
    ITSME_EXECUTABLE = 'itsme_lib.so'
    if operating_system == 'nt':
        ITSME_EXECUTABLE = 'itsme_lib.dll'
    ITSME_LIB = f'{abspath(dirname(__file__))}/{ITSME_EXECUTABLE}'

    def __init__(
        self,
        settings: ItsmeSettings
    ) -> None:
        global itsme_lib
        # Map the functions so we can call them
        itsme_lib = CDLL(self.ITSME_LIB)
        itsme_lib.Init.argtypes = [c_char_p]
        itsme_lib.Init.restype = c_char_p
        itsme_lib.GetAuthenticationURL.argtypes = [c_char_p]
        itsme_lib.GetAuthenticationURL.restype = Response
        itsme_lib.GetUserDetails.argtypes = [c_char_p]
        itsme_lib.GetUserDetails.restype = Response
        # Initialize the library
        settingsJson = dumps(settings.__dict__)
        response = itsme_lib.Init(bytes(settingsJson, 'utf-8'))
        if response:
            error = Error(response.decode('utf-8'))
            raise ValueError(error.message)

    def get_authentication_url(self, config: UrlConfiguration) -> str:
        global itsme_lib
        url_config = dumps(config.__dict__)
        response = itsme_lib.GetAuthenticationURL(bytes(url_config, 'utf-8'))
        if response.error:
            error = Error(response.error.decode('utf-8'))
            raise ValueError(error.message)
        return response.data.decode('utf-8')

    def get_user_details(self, authorization_code: str = None) -> User:
        global itsme_lib
        response = itsme_lib.GetUserDetails(bytes(authorization_code, 'utf-8'))
        if response.error:
            error = Error(response.error.decode('utf-8'))
            raise ValueError(error.message)
        return User(response.data.decode('utf-8'))
