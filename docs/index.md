# Projeto Arroba

## A iniciativa
Iniciado em 2021 pelo analista de sistemas ([#brenodpm](https://github.com/brenodpm)), o **Projeto Arroba** traz um sistema 100% em código aberto, voltado aos suinocultores de pequeno e médio porte.
A centelha do projeto se deu quando o autor resolveu se aventurar na suinocultura e apesar de uma gigantesca bibliografia a este respeito, não encontrou sistemas de controle de qualidade e que ao mesmo tempo fosse de código aberto.

# Projeto Arroba - WEBAPI
Todas as regras de negócio estarão na WebApi, ficando os sistemas clientes como meros expositores das mesmas.
A conexão será realizada por RestFull, pra tal aplicaremos o nivel 3 de maturidade;

## Arquitetura da API
Serão utilizadas três camadas no projeto, sendo elas:
1. **Application:** Esta camada será responsável por expor os métodos a serem utilizados, fazer tratamentos de seguranças e compor os retornos com os links disponíveis a parti da requisição apresentada;
2. **UseCase:** Nesca camada, cada caso de uso será gerado em uma classe distinta, contendo em si toda a regra de negócio daquela função, podendo, quando o caso de uso indicar, invocar outros casos acessórios;
3. **Infra:** Esta é a camada de acesso à dados;

**Obs.:** Uma quarta camada existirá **Domain** que proverá padrões às demais.

# Documentação

## Casos de Uso
- [CASO DE USO 001 - VALIDAR JWT](./uc/uc001-validar-jwt.html)
- [CASO DE USO 002 - VALIDAR ACCESS TOKEN](./uc/uc002-validar-access-token.html)
- [CASO DE USO 003 - EFETUAR LOGIN](./uc/uc003-efetuar-login.html)
- [CASO DE USO 004 - Logout](./uc/uc004-logout.html)
- [CASO DE USO 005 - REFRESH TOKEN](./uc/uc005-refresh-token.html)
- [CASO DE USO 006 - OUTRAS REQUISIÇÕES](./uc/uc006-outras-requisicoes.html)
## Politicas
- [GERAR JWT](./policy/gerar-jwt.html)