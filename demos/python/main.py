from flask import Flask, Response, request, jsonify
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

@app.route("/login")
def login():
    config = itsme.UrlConfiguration(['profile', 'email'], 'my_service_code', '')
    itsme_auth_url = _get_itsme_client().get_authentication_url(config)
    print('Auth URL: ' + itsme_auth_url)
    body = {
        'url': itsme_auth_url
    }
    resp = jsonify(body)
    return resp


@app.route("/jwks.json")
def hello():
    jwks = ""
    with open('../keys/jwks_public.json') as f:
        jwks = f.read()
    resp = Response(jwks)
    resp.headers['Content-Type'] = 'application/json'
    return resp


@app.route("/redirect")
def redirect():
    code = request.args.get('code')
    user = _get_itsme_client().get_user_details(code)
    return user


if __name__ == '__main__':
    app.run(debug=True)
