# CASO DE USO 003 - EFETUAR LOGIN
## OBJETIVO
Gerar uma chave de acesso ([Access Token]), ao APP utilizando pelo **suinocultor** para que possa consumir as APIs do sistema.

## REQUISITOS
- Ter uma empresa previamente cadastrada no sistema;
- Ter um usuário previamente cadastrado no sistema e vinculado a uma empresa e um grupo;
- O APP possuir uma *API-KEY* e uma *API-SECRET* válidas.

## ATORES
- Suinocultor.

## PRIORIDADES
Alta

## PRE-CONDIÇÕES
Não haver usuário logado.

## FREQUÊNCIA DE USO
Uma vez a cada início de sessão

## CRITICALIDADE
Alta

## CONDIÇÃO DE ENTRADA
APP solicita o Login ao usuário.

## FLUXO PRINCIPAL
1. O **suinocultor** solicita efetuar o *login*;
2. O **APP** solicitará usuário e senha do suinocultor;
3. O **APP** usará a criptografia MD5 para gerar um *hash* da senha que será enviada na requisição no lugar da senha real;
4. O **APP** gerará um JWT [Gerar JWT] ([RN01](#RN01));
5. O **APP** chamará o método de *Login* da **API** ([RN02](#RN02));
6. O ***Middleware* de autenticação** validará o JWT [UC001];
7. O ***backend* de segurança** validará o usuário e a *HASH* da senha [[A1](#A1 - Usuario ou senha inválido)];
8. Quando houver sessão ativa para este usuário no dispositivo informado (dados presentes no JWT), o ***backend* de segurança** desativará a sessão;
9. O ***backend* de segurança** criará uma nova sessão para o usuário no dispositivo informado;
10. O ***backend* de segurança** retornará um [Access Token] ao **APP**.

## FLUXOS ALTERNATIVOS
### A1 - Usuario ou senha inválido
1.  O ***backend* de segurança** retorna o *status code* **401** (*Unauthorized*)  com a descrição: Usuário ou senha inválido.

## REGRAS DE NEGÓCIO
### RN01
O JWT gerado pelo **APP** para a requisição de *login* deve ser incluído no *header autorization* precedido do termo **"bearer:"** (inclui dois pontos) e a *claim accessToken* será suprimida nesta requisição;
### RN02
Usa-se o verbo *POST* para o login;


[//]: # (REFERENCE LINKS)

[Gerar JWT]: <../policy/gerar-jwt.md>
[Access Token]: <../policy/access-token.md>
[UC001]: <./uc001-validar-jwt.md>