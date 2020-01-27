from flask import Flask, Response, request, jsonify
from json import dumps
import itsme

app = Flask(__name__)

def _get_itsme_client():
    private_jwk_set = ''
    with open('../keys/jwks_private.json', 'r') as jwks_file:
        private_jwk_set = jwks_file.read()
    client_id = 'my_client_id'
    redirect_url = 'https://example.com/production/redirect'
    environment = itsme.PRODUCTION
    settings = itsme.ItsmeSettings(client_id, redirect_url, private_jwk_set, environment)
    return itsme.Client(settings)

@app.route("/production/login")
def login():
    request_uri = 'https://example.com:443/production/request_uri'
    config = itsme.UrlConfiguration(['profile', 'email', 'address', 'phone'], 'my_service_code', request_uri)
    itsme_auth_url = _get_itsme_client().get_authentication_url(config)
    body = {
        'url': itsme_auth_url
    }
    resp = jsonify(body)
    return resp


@app.route("/production/jwks.json")
def hello():
    jwks = ""
    with open('../keys/jwks_public.json') as f:
        jwks = f.read()
    resp = Response(jwks)
    resp.headers['Content-Type'] = 'application/json'
    return resp


@app.route("/production/redirect")
def redirect():
    code = request.args.get('code')
    user = _get_itsme_client().get_user_details(code)
    resp = Response(dumps(user.__dict__, default=lambda o: o.__dict__))
    resp.headers['Content-Type'] = 'application/json'
    return resp


@app.route("/production/request")
def request_uri():
    url_config = itsme.UrlConfiguration(['profile', 'email', 'address', 'phone'], 'my_service_code', '')
    config = itsme.RequestURIConfiguration(url_config, 'tag:sixdots.be,2016-06:acr_advanced', 'nonce', 'state', ['tag:sixdots.be,2016-06:claim_city_of_birth', 'tag:sixdots.be,2016-06:claim_nationality', 'tag:sixdots.be,2017-05:claim_device', 'tag:sixdots.be,2016-06:claim_eid', 'tag:sixdots.be,2017-05:claim_photo'])
    data = _get_itsme_client().create_request_uri_payload(config)
    resp = Response(data)
    resp.headers['Content-Type'] = 'text/plain; charset=utf-8'

    return resp


if __name__ == '__main__':
    app.run(debug=True, port=8090)
