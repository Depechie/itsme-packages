from flask import Flask, Response, request, jsonify
from json import dumps
import itsme

app = Flask(__name__)


def _get_itsme_client():
    private_jwk_set = ''
    with open('../keys/jwks_private.json', 'r') as jwks_file:
        private_jwk_set = jwks_file.read()
    client_id = 'client_id'
    redirect_url = 'https://example.com/redirect'
    environment = itsme.AppEnvironments.PRODUCTION
    settings = itsme.ItsmeSettings(
        client_id, redirect_url, private_jwk_set, environment)
    return itsme.Client(settings)


@app.route("/login")
def login():
    request_uri = 'https://example.com:443/request'
    redirect_uri = 'https://example.com/redirect'
    config = itsme.UrlConfiguration(
        ['profile', 'email', 'address', 'phone'], 'service_code', redirect_uri, request_uri)
    itsme_auth_url = _get_itsme_client().get_authentication_url(config)
    body = {
        'url': itsme_auth_url
    }
    resp = jsonify(body)
    return resp


@app.route("/jwks")
def jwks():
    jwks = ""
    with open('../keys/jwks_public.json') as f:
        jwks = f.read()
    resp = Response(jwks)
    resp.headers['Content-Type'] = 'application/json'
    return resp


@app.route("/redirect")
def redirect_url():
    code = request.args.get('code')
    redirect_uri = 'https://example.com/redirect'
    redirect_data = itsme.RedirectData(code, redirect_uri)
    user = _get_itsme_client().get_user_details(redirect_data)
    resp = Response(dumps(user.__dict__, default=lambda o: o.__dict__))
    resp.headers['Content-Type'] = 'application/json'
    return resp


@app.route("/request")
def request_uri():
    url_config = itsme.UrlConfiguration(
        ['profile', 'email', 'address', 'phone'], 'service_code', 'https://example.com/redirect', '')
    config = itsme.RequestURIConfiguration(url_config, itsme.ACRValues.ACRAdvanced, 'nonce', 'state', [
                                           itsme.Claims.CityOfBirth, itsme.Claims.Device, itsme.Claims.Eid, itsme.Claims.Nationality])
    data = _get_itsme_client().create_request_uri_payload(config)
    resp = Response(data)
    resp.headers['Content-Type'] = 'text/plain; charset=utf-8'
    return resp


@app.route("/")
def hello():
    return "Hello!"


if __name__ == '__main__':
    app.run(debug=True, port=8090)
