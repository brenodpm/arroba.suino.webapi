# GERAR JWT
O JWT deve ser gerado pelo cliente em cada requisição com as seguintes *claims*:

| *CLAIM* | DESCRIÇÃO | OBRIGATÓRIO |
| ----- | --------- | ----------- |
| iss | *ApiKey* do *client* utilizado | sim |
| iat | Data da emissão (*Epoch*) | sim |
| exp | Data da expiração (*Epoch*) (30s) | sim |
| jti | *Nonce* | sim |
| accessToken | Código de sessão emitido pelo método de *login* | não |
| body | *HASH* MD5 do conteúdo do *body* | sim |


## Como montar
1. Capturar a *string* do *body* e gerar um *HASH* utiliando a criptografia **MD5**;
* importante garantir que todos os caracteres envaidos no *body* sejam os mesmos ao gerar o *HASH*.
2. Informar a *API-KEY* do *client* atual;
3. Gerar a data de envio utilizando o padrão *Epoch Java*, também a data de expiração considerando 30 segundos a mais que a data de envio;
4. informação de um valor único para cada resquisição;
5. Exceto na requisição de *login*, todas as requisições devem conter o *accessToken* recebido na requisição de *login*;
6. O *JWT* deve ser assinado com a *ApiSecret* do *Client*, utilizando o algoritmo **HMACSHA256**.

## Header
O *JWT* deve ser incluído no *header* *authorization* contendo o valor: "bearer" + " " (espaço) + JWT. Segue exemplo:

```sh
bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c
```