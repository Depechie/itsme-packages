package main

import (
	"encoding/json"
	"fmt"
	"github.com/itsme-sdk/itsme-golang"
	"io/ioutil"
	"log"
	"net/http"
)

func sayHello(w http.ResponseWriter, r *http.Request) {
	fmt.Fprint(w, "hello world")
}

func servePublicKeys(w http.ResponseWriter, r *http.Request) {
	jwks, err := ioutil.ReadFile("../keys/jwks_public.json")
	if err != nil {
		fmt.Fprint(w, err.Error())
		return
	}
	w.Header().Set("Content-Type", "application/json")
	fmt.Fprint(w, string(jwks))
}

func buildLoginURL(w http.ResponseWriter, r *http.Request) {
	itsmeClient, err := getItsmeClient()
	if err != nil {
		fmt.Fprint(w, err.Error())
		return
	}
	config := itsme.URLConfiguration{
		Scopes:      []string{"profile", "email", "address", "phone"},
		ServiceCode: "MY_SERVICE_CODE",
		RequestURI:  "https://example.com:443/production/request_uri",
		RedirectURI: "https://example.com/production/redirect",
	}
	url, err := itsmeClient.GetAuthenticationURL(config)
	if err != nil {
		fmt.Fprint(w, err.Error())
		return
	}
	body := fmt.Sprintf("{\"url\":\"%s\"}", url)
	w.Header().Set("Content-Type", "application/json")
	fmt.Fprint(w, body)
}

func handleRedirect(w http.ResponseWriter, r *http.Request) {
	code := r.URL.Query().Get("code")
	if code == "" {
		w.WriteHeader(http.StatusBadRequest)
		fmt.Fprint(w, "no code found on request")
		return
	}
	itsme, err := getItsmeClient()
	if err != nil {
		w.WriteHeader(http.StatusInternalServerError)
		fmt.Fprint(w, err.Error())
		return
	}
	user, err := itsme.GetUserDetails(code)
	if err != nil {
		w.WriteHeader(http.StatusInternalServerError)
		fmt.Fprint(w, err.Error())
	}
	userJSON, err := json.Marshal(user)
	if err != nil {
		w.WriteHeader(http.StatusInternalServerError)
		fmt.Fprint(w, err.Error())
	}
	w.Header().Set("Content-Type", "application/json")
	fmt.Fprint(w, string(userJSON))
}

func handleRequest(w http.ResponseWriter, r *http.Request) {
	itsmeClient, err := getItsmeClient()
	if err != nil {
		w.WriteHeader(http.StatusInternalServerError)
		fmt.Fprint(w, err.Error())
		return
	}
	config := itsme.RequestURIConfiguration{
		URLConfiguration: itsme.URLConfiguration{
			RedirectURI: "https://example.com/production/redirect",
			Scopes:      []string{"email", "picture", "profile", "address"},
			ServiceCode: "MY_SERVICE_CODE",
		},
		AcrValue: itsme.ACRAdvanced,
		Nonce:    "2345yhgfdswertyhgfds",
		State:    "thisismyriflethisismygun",
		Claims:   []itsme.Claim{itsme.ClaimCityOfBirth, itsme.ClaimEid, itsme.ClaimDevice, itsme.ClaimNationality, itsme.ClaimPhoto},
	}
	jwt, err := itsmeClient.CreateRequestURIPayload(config)
	if err != nil {
		w.WriteHeader(http.StatusInternalServerError)
		fmt.Fprint(w, err.Error())
		return
	}
	fmt.Fprint(w, jwt)
}

func getItsmeClient() (*itsme.Itsme, error) {
	jwks, err := ioutil.ReadFile("../keys/jwks_private.json")
	if err != nil {
		return nil, err
	}
	settings := itsme.Settings{
		ClientID:       "my_client_id",
		PrivateJWKSet:  string(jwks),
		AppEnvironment: itsme.PRODUCTION,
	}
	return itsme.NewItsmeClient(&settings), nil
}

func main() {
	http.HandleFunc("/", sayHello)
	http.HandleFunc("/production/jwks.json", servePublicKeys)
	http.HandleFunc("/production/login", buildLoginURL)
	http.HandleFunc("/production/redirect", handleRedirect)
	http.HandleFunc("/production/request", handleRequest)
	log.Fatal(http.ListenAndServe(":8090", nil))
}
