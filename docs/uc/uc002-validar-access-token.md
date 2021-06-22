# CASO DE USO 002 - VALIDAR ACCESS TOKEN
## OBJETIVO
Verificar a integridade do *Access Token* informado no JWT

## REQUISITOS
- A requisição possuir um JWT válido com *access token* na *claim* *accessToken*.  

## ATORES
Todos

## PRIORIDADES
Alta

## PRE-CONDIÇÕES
Informar se será validado o *access token*

## FREQUÊNCIA DE USO
Em toda requisição que consome o *backend*

## CRITICALIDADE
Alta

## CONDIÇÃO DE ENTRADA
Qualquer chamada ao passar pelo ***Middleware* de autorização** da API.

## FLUXO PRINCIPAL
1 - O ***backend* de segurança** busca a Sessão pelo código contido no *access token* [[A1](#A1 - Sessão inexistente)] [[A2](#A2 - Sessão desativada)];
2 - O ***backend* de segurança** busca a código de segurança da empresa pelo código contido no *access token*;
3 - O ***Middleware* de autenticação** valida a assinatura do *access token* pela *secret* retornada da empresa[[A3](#A3 - Assinatura inválida)];
4 - O ***backend* de segurança** busca o grupo de acesso, pelo código contido no *access token*;
5 - O ***Middleware* de autorização** verificará se no grupo de acesso contem a claim exigida pela API [[A4](#A4 - Claim não encontrada)];
6 - O ***backend* de segurança** atualiza as informações de ultimo acesso da sessão.


- Atualizar Sessão

## FLUXOS ALTERNATIVOS
### A1 - Sessão inexistente
1 -  O ***backend* de segurança** retorna o *status code* **401** (*Unauthorized*) com a descrição: Sessão expirada.
### A2 - Sessão desativada
1 -  O ***backend* de segurança** retorna o *status code* **401** (*Unauthorized*) com a descrição: Sessão expirada.
### A3 - Assinatura inválida
1 -  O ***Middleware* de autorização** retorna o *status code* **401** (*Unauthorized*) com a descrição: Terminal de acesso inválido.
### A4 - Claim não encontrada
1 -  O ***Middleware* de autorização** retorna o *status code* **401** (*Unauthorized*) com a descrição: O corpo da mensagem não condiz com o cabeçalho informado.

## REGRAS DE NEGÓCIO