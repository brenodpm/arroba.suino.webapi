# CASO DE USO 004 - LOGOUT
## OBJETIVO
Encerrar sessão no dispositivo atual

## REQUISITOS
- Ter sessão ativa no dispositivo

## ATORES
- Suinocultor.

## PRIORIDADES
Normal

## PRE-CONDIÇÕES
Haver usuário logado.

## FREQUÊNCIA DE USO
Uma vez a cada final de sessão

## CRITICALIDADE
Baixa

## CONDIÇÃO DE ENTRADA
Suinocultor clica em Logout

## FLUXO PRINCIPAL
1. O **suinocultor** solicita efetuar o *logout*;
2. O **APP** gerará um JWT [Gerar JWT];
3. O **APP** chamará o método de *Logout* da **API**;
4. O ***Middleware* de autenticação** validará o JWT [UC001];
5. O ***Middleware* de autorização** validará o *[Access Token]* [UC002];
6. O ***backend* de segurança** desativará a sessão ativa.

## FLUXOS ALTERNATIVOS

## REGRAS DE NEGÓCIO


[//]: # (REFERENCE LINKS)

[Gerar JWT]: <../policy/gerar-jwt.html>
[Access Token]: <../policy/access-token.html>
[UC001]: <./uc001-validar-jwt.html>
[UC002]: <./uc002-validar-access-token.html>