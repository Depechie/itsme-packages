const itsme  = require('../../nodejs/index');

const privateJwkSet = `{
    "keys": [
        {
            "kty": "RSA",
            "use": "sig",
            "n": "AMtV9Y3P3yqLrXxhDDZXo4Ec8gJ0R1HWGenC93PTC9NtQQdH2yXedWb-0OF1tBXRNrBa-6fJTLRENsFQF7qiC7NNZDavFuVheCk379c-cMm00ZwTKl320a3Deo_WtDYHXQ2KUgE14jc_5O44_J08PFxI0Q2zDgN5XMsM5hpxBfpT6b1-7tg5rqseae4s6q5cfOLNytNyBqSCItJliT2jDEz7dLOCf-whrWSddnybIKIvJGX6fYSz4jTm3dwgufo2sEFJ9aVD3i410BZoQEPRX98qOOps58sSahN2NpkS2en9EhVryrZGZ0RlV5QcJdevuOT3Ol-n7J7fZzGqlRlvTHSxt9AbRk_o_83AgLakwZZ3iYJIwKEoNSDGNPOcyGLxCl45yjC06SmcFUiurH_dfUJPeFINyGPbC-vUn4cftcS2pQOZyJjopY58-YkvyU3wQwBsejBlGEvRhIkcxax--4XZAB5hr4edU7fHWO3gC8bMIp-OSQHwF7nhfHcU0bMkL5kk3FtfSNdHzvV1O_CH7hWULqV5UupD3dzZuCXcyfC5Gf6UhNNeFobV66gnBr_E-iG-Xx9Gp6NnVGzT-64IQAgHW8WZy4EKjzqz4Hx86YHjaogyRw0DYDUNw_adC4IhkBouGXRZ7W3-75zIx0IpuimnIswaFws-HiKmTMQHxTKP",
            "e": "AQAB",
            "d": "CRjLZ5DJNyoQlOPym-vH0NruqNVy62JcvmyOjYv_l3Hn2t-IGmWdcwIrbirJ57_4ZmNrptIGsHnsyTFAHoVpaJgpx5iFubVmntF11XdhrGR-jDD_UDzenJuGPDEq1s_1hcmOaadze21MXKwIATDUsnw_fhFoqxce46asFLrBvbVLW8L2BI5wFg_CRXmU9kAFBlCWpmcPjQSVcjxAq5VD053cVrub5YCMNVgmGOUHzLm3MyjFvM4fbKZ3foWoTF1t_T3F06L5qa-Wo6n-Vgpj86P77-D3bcphHIXJY4fIYAOGb5bm9sinjhPKm0G3H7Mo3nWBEw58M49jGoG7Mewcy3dLvcx9SY6c5ajtJvxNpFpHyKz6w0mWYjdp4NlzdDEWxT4tcmjPwDJx3_6H0UvvWt2NM-VQnZbnmNlN8Tie92QGcHd3PtBux56TtSLCXjd5LIFSojSME93xQq5XExOpwV1rcy115eSpZ2zllwT2V-u8c3D8uoptMuAZO26PkDIyQd2oZ-kT_SGjwkmfFl8a9PV39HRVF-4SeFq5Z54GPqaXLt8s5J3Kxhxk43VdSbx6GD3Rd-J3e51sxA95quSGxBq7x51ayU0U5uL5GQqj0CvrhekECEYc6ug0shULwNmT4ATf0LB1q0jO0lDBJTqt1ciG80fpLI5fVYjB9DSbKOE",
            "p": "AOqvCPZaoVvNhVQrwUAjSb_w4dQEifAt8WIQE5qDh0lz7U5UB_-130gvHlcncy1DP2U5YyXuLlV7q6IwV3EEZ_JceYxIw0TypQN3WyFoBh4zqawuXzpWFblmGv3bHgSHWlpVi2HJwsPnIHfaxfvH4Ngg0veGZ01_QwQT9Z6G80-07mNB76EhijVGGbvwEYliaKjtUMFsB9QfE1x2h2Cmy6SpS454tOckLQeDt8SOZmoXYtZ2efBZjct8W3WYYhbK5-G3wu5VeGj98-LJS1PVAyLA3hNYeWZyOF--3mKJHDgAUvvA71mF5nCZggtn42mjvyAawzcUsx3aF70Sjw6eVrs",
            "q": "AN3OAiWy7LoJDDFpkm2k4M5JFsjz8oReKlhC4zXucy59F23YmJ6o5kApAiSLs01VAiMq5k3J49bVxeGDkkvWuyb2EsrRXCDwuYJMa6hxQsUw3mQrYMe4vtLedomiI15w79haMdQlvPE8CancHH4qU2-b49zxy0EtevFOWl071xN6qV3eYzsxlexjfrPwBN7_RYEA5q383UVGyFZWBTbSwitUobRX7KMBYIPBArK7e8bRkdw798QGTnULRyHRGoHm7QaRS5Bb9RWs3sBT9c-Y9ury2N-MUynsJtdEG-sRxjBlYSZ8WxFPzYn2IyijplyiE1upDjmVVmtdMl5FTH-rGD0",
            "dp": "AMLlXdIJbhupUGKYe8LiC2tFhqXmpw85eE7x_vXA6WdMyPTVwFbSGX3-83l5n6MlR86um_JyHTSMofrtnpCTEigqOB8ShmgKApQgWSQjGYGXcf81-4uc1inD7AKKKCGmrph6lmg9gGvk1Af1PZpdYhLbH5jEm6G2YFdQpBhlBkdZgOgZaAIusnKPput28Xlgqn4vp3MaXl4A-Lk_DipsGo7Csaa-A645UtP8xW6Mu-y7Qy_L8HR0SteEjQfZ5wv6shu32kDH5ll7HUFn3hDew93mwgba4Ob9UGfjQRgfubmgO0or7howozE7qs4CFtAyVpU45IonvKC78B7q_LctM-U",
            "dq": "ANNx7EIcC-hdb647QkgcAeMlcf8n722Yhy2Uth1Xr54FyiwBa5z0zmsjrNNCCjASdfMollxZtkW7jOSrFPAPSGKf-tI2O2l7kp1QVOFC97AcIiXD5IMA74j-YmpakvUZHMxXT2E6iKA_58Zx-webOecP6W1bY7RCsw0O8L4oqaP8LzE5K8fmQVIUl1jZ2wrqt0t_nGkIwbek_p6valEVId1E1NKjbwcF7QgtG3FfoWOrrS0STeQlTu3tTmZCfM6RYiv8ntiDkWSUY9VdNNwuqlTYJT8W-sDgSzazyxmQ3CqOusqBih86wGpgpqu7NoDgUSv0QYdLghLfyYBs97xNUtE",
            "qi": "AOGq8jDebxEKDDx4bhZTJpFV05DMQ_EtG7h2uyT1uwUKJTNW0nNAWFDwqlfWoKMa2XceZH2rbaQEs2aKVUzCmTvheHjaFsaqn7fSqyHperyiZPG5uFWSVUUILPOBSqT-E0kQI7VacrDaMSzzbGC6_DOuRMiuv_Bb4Q8ATOlgP5vZOMwznEgybtBO7pHPTRUCl77e8iF7pDyXXofzutK01XwXC8EJaihBSbVA9tni0iUCe0DZUP5J0kyJpZYXiOjsn6AOfWLEddvi1j-PowRlyXTDzOm9PaAKuFLKIaOEWHeG2Jq-2m2MpfHxjvwXDHToLvseLvyFmHlzN0l7zouGxlc",
            "kid": "excellent-gun",
            "alg": "RS256"
        },
        {
            "kty": "RSA",
            "use": "enc",
            "n": "AJ9iKE2SheSqyAvDMEAGjNfv0v48NjB7yttv__zoVqcp6a3zItFK6R5r9bAxgLA7KRzVmkeGTsd5jyRbYHxsWqfT1mvnd1KC4zWEjWaRJBqsTPmMizUBaJd7GXc6yM42HTf_yAl1R-UvmLq6f5hzV6Sg3ulW7c6fOdscGvAmm16lrAE5k4gPLlSAHvFTsfV5IJxA0DQOrJsKBXo1994PmHKvYNn_6JkeRGfioH45KmDyFalx6ZxA5XSM4c3AvKgXguKeZKE8aWPfE1QHgeEsMqAltcevhtPQvyyG2kBh8pbFvNhkyMRd-Mb4HxS-6GEh1hDMIPXb2ifxgdRpNnaIFj82D3ARTalYB8ibv75VCGCeY_iFN63McWYmkH3j16_rtfgGmlTVGZ3Uj9Z4ifiptdCg0OaittEGW9W7MwcUVXn1b6zhu6zJvjTVH8stbFcfMh27LETVFswm9ISjlZlQFSYT2okMoDJCOtRAgrn8qQHwCUjG6l2NGhSWA4mzZeeygsXhSHTNuxa-2yEpJQTM9fDaJqRdAQikL9LCOi7VqaBg9dpazmC0yC19oEkybwrEu0OCcrI8RVq-iecZEOiWHCpVZuBZAZa8vETe07YxDv4DvwnXZMtVzAeTcfx7ZD13SWYS1fM7PEjRlcrfK3LH7nTV1sM8H4FkcyCw01hdniWL",
            "e": "AQAB",
            "d": "Gc92gWFqB2bUsHrydFvbMRjoTT2I9P8HEdQoW1dwuO9jpUTLbaM47zFCfz70_RUHimDY9B4-4bTAxn15uYx-fdEjVyQLwbPqgX8uut5kP2GJsZvd6z6IJofLzvAZFrPStX_ZcBarBuVXUV3kTdbX6ZQXpSfrqNtGLkJ15lAKNyCTIzuktZLnyM1atW_9UKhF7KfeT5_UueK8klbtBev2nxPhlkwH6cvp_fzISsiNIVfyxBbrYt9riR1Lb9t3ca0lB_foiefTlVXJtoDkuodCx4Zz0puNNP4IB3jrHSP_egUZmN1xWcNyBx6lQeHMlfY5yUyVRdtNj8xfXvuO01EkcOSpyEM1vT9U49vAA12IGY5emO6U_5d1lzuPmBRu1wTxYu2qHsZrakFCUBtC1X6OtgAvUeOKlPvHBCmdL7i2UOrvCyPlVKXcSn7fV08vsqbtM3ai6N2QhRlXtWzPyJQpAIoPDLLScF7DLv0kqWaGe_Q31aRa1RxY6r-qfutKJakTM77ayNS2F8GN0p8NCXDC51g_nnpbV0rtpNF2N4kkWE8qq97CIwHez6rsoXUxdXPGEnWWQ68NKDF3pLj9FcNXShaJ61DSz7cNkawc88Ch7OfgEUTICd8z424ZpFO6qQtI8c1xbJ8-WGOWcn4Lt0EOXc5P7fcXZzMHOWO1C8DfWCk",
            "p": "AM8c3ZIZRfRbmToMiYxdCj0iWlHh0ut3Gll6DGg-_O2TW_n_axpEetUFlt_duR-SlOr1hu2R66zVk20FIvCpbXD3r8VWGL0OIHm06ZZlOf1MIYkJF95SJIJT6FuYByC4-PnAxC1aLpX3Ezk_3sLAcbmFhQdMugz_bV9_iaLfdPAL_UMcFTyYKhHVVsjsMJB16lqnG9zhdo6ULwcyNP-yhHmHYZoka1agLwqFWZO-zGx6D39v9RqVq-USMKEiFxukZlZOwc4mSBgFUA0HnVnLR8h8_xJa9gET6X-pcwsIhOl2gltsKiLxC9u-ZyXX3zIGkJvM3HTqja8j-FiSP2PJ5Y0",
            "q": "AMUBKt8ozIhbnXrvHvDeTcHb0YzyqezwAPLqBTm5FVEN_Skdiy9oEVlnC-oh0lUvsMYbY-JJt3AmeXrX9nlZqMUEtgCrhGPjVC2jnDpzKELsj5TPE5OrGCX-sHt8DdUbqKZsrhSgaqRjQzQWSoSEwv7fMB8HR2zCE8Tl6zqXW8ptZ5_fh729GNOUJovwKeVp504x6tHZ0fOGqPyLPpKxlJIjjMNRLGNxdGKlsm2gq3CnImlQ6Nsil11FS2cToiqHQ-qK6pdZNSyuD-pcoq4rYnBNLsMsdLHn4vrvxaMJngWWLIeNDKSJSYFKeHsEFuI1-cZb6EwbTdCza4Y-Q6YHdXc",
            "dp": "AL__UF_p0fHPIGKx0KkwbxdxSy1xHShvhxu_itTw32QWIUPGRjFZnkzwG4d4ce8U4bxW1DhT95iHfV2pT1Sum-gHlQho4hIQkIaC8bAcMv_fnY79wQS2PWYcBuEyNBlfoF9XFgW8_kbDfyypSwZbhJGpd0LnG-qEt426G4vDfxIw6x1VYVKE9SsHDKbCXdrtlM1o8Ol4kYFnWZlgL8WCZlCK8i4E6QSrBjDIodvXGobNOKS40w1MBsgabr5Rbf6QDtafuAY1csvh0O3jSzk8u1Tem08Wx8sD_If-O8nUofITjJDX2s_9iTKydcidMCmd8FyeMxinf6UGwWjKWr1VItk",
            "dq": "E4evGVpxFF-IuCQ2fAh9yucg2glAgUW1wl8iz3siuttZKpdMMhHuDl_h1X8u29UTgEA84bUPI2YdCumX_KW_znBOHrjiMgjaUn_yv_SknCXZghYZSb1fcnzSQBunP2YzjPQfOdpBWDSi9-nxzzteHWhA3AKbuRNwZEeqzy8sIWBBMp81VMEHUgpQ2_SeIqkiDiVyswUWBD9oR0kClgZWxToS_VXxwZYAxLrKeoXAGTXoempT-01tlgiFlh1jKTYprHnDEef9FoXW_BPc6MlSQ_HS3fRtSSnE3b2haS1N5h3iIjoAc9tMVy3rzb41Co3xShHYcJsQVs1CrpdDm5UHtw",
            "qi": "AbGKH5jlwbjv65OjibNf6X8I78DLi9q9rZuXoRHGE40nJpLXiYXj6eAE95qgGfdbaNSxUYzGEDQKmgR5ezOSPQKh9nE0ns4V0v2ZSNwz-S1mYDKtj817sI2ANN-iBDBjmuxaejua6620vHtzCJWCaIdwqUVNY1PbjHaWDzXaLABxE06K28msy2AdIz1yg-IXtTjZsxKdU4JXaQK2s4z6CMcFHGI6mnwuL57kKTBhjKu1OjAWL9VYsb6wrPmHlEqDPNt7rV9apNnN5eu8VIbp6-K-7ahGgGUlI2J8Rn0LSvOP--o0RF8vbJjM8oBJlwLqfIFuKGulYVkFcxZQHJ67nQ",
            "kid": "puny-twist",
            "alg": "RS256"
        }
    ]
}`;
const clientId = 'my_client_id';
const redirectUrl = 'https://i/redirect';

const settings = itsme.createItsmeSettings(clientId, redirectUrl, privateJwkSet);
const client = new itsme.Client(settings);
const config = itsme.createUrlConfiguration(['profile', 'email'], 'my_service_code', '');
const itsmeAuthUrl = client.getAuthenticationUrl(config);
console.log(itsmeAuthUrl);
var user = client.getUserDetails('authorization_code_I_received_from_itsme');
console.log(user);
