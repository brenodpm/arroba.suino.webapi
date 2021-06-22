# CASO DE USO 001 - VALIDAR JWT
## OBJETIVO
Verificar a integridade do JWT informado na requisição

## REQUISITOS
- A requisição possuir o cabeçalho *autorization*;
- O Cabeçalho *autorization* conter o termo *bearer* sussedito pelo *token* JWT separados pelo caracter ':' (dois pontos).  

## ATORES
Todos

## PRIORIDADES
Alta

## PRE-CONDIÇÕES
Informar se será validado o *access token*

## FREQUÊNCIA DE USO
Em toda requisição

## CRITICALIDADE
Alta

## CONDIÇÃO DE ENTRADA
Qualquer chamada ao passar pelo ***Middleware* de autenticação** da API.

## FLUXO PRINCIPAL
1 - O ***backend* de segurança** busca o cliente pela *API-KEY* [[A1](#A1 - Cliente inexistente)] [[A2](#A2 - Cliente desativado)];
2 - O ***Middleware* de autenticação** valida a assinatura do JWT pela *API-Secret* do Cliente [[A3](#A3 - Assinatura inválida)];
3 - O ***Middleware* de autenticação** validará a integridade do *body* recebido com o *HASH* presente na *claim body* do JWT [[A4](#A4 - Body inválido)];


- Atualizar Sessão

## FLUXOS ALTERNATIVOS
### A1 - Cliente inexistente
1 -  O ***backend* de segurança** retorna o *status code* **401** (*Unauthorized*) com a descrição: Terminal de acesso inválido.
### A2 - Cliente desativado
1 -  O ***backend* de segurança** retorna o *status code* **401** (*Unauthorized*) com a descrição: Terminal de acesso inválido.
### A3 - Assinatura inválida
1 -  O ***Middleware* de autenticação** retorna o *status code* **401** (*Unauthorized*) com a descrição: Terminal de acesso inválido.
### A4 - Body inválido
1 -  O ***Middleware* de autenticação** retorna o *status code* **401** (*Unauthorized*) com a descrição: O corpo da mensagem não condiz com o cabeçalho informado.

## REGRAS DE NEGÓCIO