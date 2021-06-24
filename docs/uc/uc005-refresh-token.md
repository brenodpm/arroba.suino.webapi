# CASO DE USO 005 - REFRESH TOKEN
## OBJETIVO
Revalida para um novo período a sessão atual

## REQUISITOS
- Ter sessão ativa no dispositivo

## ATORES
qualquer.

## PRIORIDADES
Normal

## PRE-CONDIÇÕES
Haver usuário logado.

## FREQUÊNCIA DE USO
Uma vez a cada final de sessão

## CRITICALIDADE
Baixa

## CONDIÇÃO DE ENTRADA
Quando faltar um dia para a sessão vencer

## FLUXO PRINCIPAL
1. O **suinocultor** solicita efetuar o *logout*;
2. O **APP** gerará um JWT [Gerar JWT];
3. O **APP** chamará o método de *Refresh Token* da **API**;
4.  O ***Middleware* de autenticação** validará o JWT [UC001];
5. O ***Middleware* de autorização** validará o *[Access Token]* [UC002];
6. O ***backend* de segurança** busca a Sessão pelo código contido no *access token* [[A1](#A1 - Sessão inexistente)] [[A2](#A2 - Sessão desativada)];
7. O ***backend* de segurança** desativará a sessão;
8. O ***backend* de segurança** criará uma nova sessão para o usuário no dispositivo informado;
10. O ***backend* de segurança** retornará um [Access Token] ao **APP**.

## FLUXOS ALTERNATIVOS
### A1 - Sessão inexistente
1. O ***backend* de segurança** retorna o *status code* **401** (*Unauthorized*) com a descrição: Sessão expirada.
### A2 - Sessão desativada
1. O ***backend* de segurança** retorna o *status code* **401** (*Unauthorized*) com a descrição: Sessão expirada.
## REGRAS DE NEGÓCIO


[//]: # (REFERENCE LINKS)

[Gerar JWT]: <../policy/gerar-jwt.md>
[Access Token]: <../policy/access-token.md>
[UC001]: <./uc001-validar-jwt.md>
[UC002]: <./uc002-validar-access-token.md>