# GERAR ACCESS TOKEN
O *ACCESS TOKEN* deve ser gerado pela *WebApi* quando a autenticação do usuário no *login* for efeturada com sucesso, transportando as seguintes *claims*:

| *CLAIM* | DESCRIÇÃO | OBRIGATÓRIO |
| ----- | --------- | ----------- |
| iss | Código da sessão | sim |

O *Access Token* é um *JWT* contendo no *body* o(s) valor(es) acima e assinado com a *Security* da empesa vinculada ao usuário, utiliza-se o algoritmo *HMACSHA256* na assinatura.