from json import loads, dumps
import typing
from typing import Dict, List, Tuple
from ctypes import CDLL, c_char_p, Structure
from os.path import abspath, dirname
from sys import platform as operating_system


class Response(Structure):
    _fields_ = [('data', c_char_p), ('error', c_char_p)]


class Error(object):
    def __init__(self, json):
        error = loads(json)
        self.message = error.get('message', 'Could not parse error object')


class User(object):
    def __init__(self, json):
        user = loads(json)
        self.aud = user.get('aud', '')
        self.email = user.get('email', '')
        self.email_verified = user.get('email_verified', '')
        self.phone_number = user.get('phone_number', '')
        self.phone_number_verified = user.get('phone_number_verified', '')
        self.family_name = user.get('family_name', '')
        self.gender = user.get('gender', '')
        self.given_name = user.get('given_name', '')
        self.iss = user.get('iss', '')
        self.locale = user.get('locale', '')
        self.name = user.get('name', '')
        self.sub = user.get('sub', '')
        self.birthdate = user.get('birthdate', '')
        self.city_of_birth = user.get('city_of_birth', '')
        if 'eid' in user:
            self.eid = Eid(user['eid'])
        if 'address' in user:
            self.address = Address(user['address'])
        self.birthdate = user.get('birthdate', '')
        if 'device' in user:
            self.address = Device(user['device'])


class Address(object):
    def __init__(self, json):
        address = loads(json)
        self.country = address.get('country', '')
        self.street_address = address.get('street_address', '')
        self.locality = address.get('locality', '')
        self.postal_code = address.get('postal_code', '')


class Eid(object):
    def __init__(self, json):
        eid = loads(json)
        self.eid = eid.get('eid', '')
        self.issuance_locality = eid.get('issuance_locality', '')
        self.national_number = eid.get('national_number', '')
        self.read_date = eid.get('read_date', '')
        self.validity_from = eid.get('validity_from', '')
        self.validity_to = eid.get('validity_to', '')


class Device(object):
    def __init__(self, json):
        device = loads(json)
        self.os = device.get('os', '')
        self.app_name = device.get('appName', '')
        self.app_release = device.get('appRelease', '')
        self.device_label = device.get('deviceLabel', '')
        self.debug_enabled = device.get('debugEnabled', '')
        self.device_id = device.get('deviceId', '')
        self.os_release = device.get('osRelease', '')
        self.manufacturer = device.get('manufacturer', '')
        self.has_sim_enabled = device.get('hasSimEnabled', '')
        self.device_lock_level = device.get('deviceLockLevel', '')
        self.sms_enabled = device.get('smsEnabled', '')
        self.rooted = device.get('rooted', '')
        self.device_model = device.get('deviceModel', '')
        self.msisdn = device.get('msisdn', '')
        self.sdk_release = device.get('sdkRelease', '')


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


class RequestURIConfiguration(object):
    def __init__(self, url_config: UrlConfiguration, acr_value: str, nonce: str, state: str, claims: List[str]):
        self.url_config = url_config
        self.acr_value = acr_value
        self.nonce = nonce
        self.state = state
        self.claims = claims


PRODUCTION = 'production'
SANDBOX = 'e2e'


class Client(object):
    ITSME_EXECUTABLE = 'itsme_lib.so'
    if operating_system == 'win32' or operating_system == "cygwin":
        ITSME_EXECUTABLE = 'itsme_lib.dll'
    if operating_system == 'darwin':
        ITSME_EXECUTABLE = 'itsme_lib.dylib'
    ITSME_LIB = f'{abspath(dirname(__file__))}/{ITSME_EXECUTABLE}'

    def __init__(
        self,
        settings: ItsmeSettings
    ) -> None:
        # Map the functions so we can call them
        self.itsme_lib = CDLL(self.ITSME_LIB)
        self.itsme_lib.Init.argtypes = [c_char_p]
        self.itsme_lib.Init.restype = c_char_p
        self.itsme_lib.GetAuthenticationURL.argtypes = [c_char_p]
        self.itsme_lib.GetAuthenticationURL.restype = Response
        self.itsme_lib.GetUserDetails.argtypes = [c_char_p]
        self.itsme_lib.GetUserDetails.restype = Response
        self.itsme_lib.CreateRequestURIPayload.argtypes = [c_char_p]
        self.itsme_lib.CreateRequestURIPayload.restype = Response
        # Initialize the library
        settingsJson = dumps(settings.__dict__)
        response = self.itsme_lib.Init(bytes(settingsJson, 'utf-8'))
        if response:
            error = Error(response.decode('utf-8'))
            raise ValueError(error.message)

    def get_authentication_url(self, config: UrlConfiguration) -> str:
        url_config = dumps(config.__dict__)
        response = self.itsme_lib.GetAuthenticationURL(bytes(url_config, 'utf-8'))
        if response.error:
            error = Error(response.error.decode('utf-8'))
            raise ValueError(error.message)
        return response.data.decode('utf-8')

    def get_user_details(self, authorization_code: str = None) -> User:
        response = self.itsme_lib.GetUserDetails(bytes(authorization_code, 'utf-8'))
        if response.error:
            error = Error(response.error.decode('utf-8'))
            raise ValueError(error.message)
        return User(response.data.decode('utf-8'))
    
    def create_request_uri_payload(self, request_uri_config: RequestURIConfiguration) -> str:
        request_uri_config.url_config = request_uri_config.url_config.__dict__
        request_uri_config_serialized = dumps(request_uri_config.__dict__)
        response = self.itsme_lib.CreateRequestURIPayload(bytes(request_uri_config_serialized, 'utf-8'))
        if response.error:
            error = Error(response.error.decode('utf-8'))
            raise ValueError(error.message)
        return response.data.decode('utf-8')
