# CASO DE USO 006 - OUTRAS REQUISIÇÕES
## OBJETIVO
Padronizar a forma de autenticação e autorização pra todos os outros métodos

## REQUISITOS
- Ter sessão ativa no dispositivo

## ATORES
- Suinocultor.

## PRIORIDADES
Normal

## PRE-CONDIÇÕES
Haver usuário logado.

## FREQUÊNCIA DE USO
Uma vez antes de cada chamada

## CRITICALIDADE
Baixa

## CONDIÇÃO DE ENTRADA
Suinocultor chama qualquer método da API

## FLUXO PRINCIPAL
1. O **suinocultor** chama qualquer método da API;
2. O **APP** gerará um JWT [Gerar JWT];
3. Se o *[Access Token]* estiver faltando um dia pra vencer, será solicitado um *Refresh Token* [UC005];
4. O **APP** chamará o método de *Logout* da **API**;
5. O ***Middleware* de autenticação** validará o JWT [UC001];
6. O ***Middleware* de autorização** validará o *[Access Token]* [UC002];
7. O ***backend*** consumirá a API desejada.

## FLUXOS ALTERNATIVOS

## REGRAS DE NEGÓCIO


[//]: # (REFERENCE LINKS)

[Gerar JWT]: <../policy/gerar-jwt.html>
[Access Token]: <../policy/access-token.html>
[UC001]: <./uc001-validar-jwt.html>
[UC002]: <./uc002-validar-access-token.html>
[UC005]: <./uc005-refresh-token.html>