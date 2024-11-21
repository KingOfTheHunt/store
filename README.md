
# Store

Projeto de geração de pedido de loja com o objetivo de praticar conceitos de Domain Driven Design e testes de unidade.


## Aprendizados

Os aprendizados com este projeto foram: **Domínios Ricos**, **Commands**, **Fail Fast Validation**, **Handler**, **Mocks** e **Red, Green e Refactor**.

- **Domínios ricos**: além de representar as tabelas que existem no banco de dados, é responsável pelas regras de negócio.

- **Commands**: O command faz parte do conceito CQRS (Command-Query Responsability Segragation). O command é a porta de entrada dos dados no domínio. Ele é muito parecido com o DTO (Data Transfer Object).

- **Fail Test Validation**: Consiste em validar os dados do command logo no início da requisição para evitar processamento desnecessário caso os dados do command não sejam válidos.

- **Handler**: Responsável por manipular o fluxo da aplicação.

- **Mocks**: São implementações falsas das dependências que as classes necessitam para funcionar. Essas falsas implementações possibilitam a execução de testes de unidade.

- **Red, Green e Refactor**: É um conceito do TDD (Test Driver Design) que consiste em fazer os testes falharem, depois passarem e por último refatorá-los.


## Rodando os testes

Para rodar os testes, basta entrar na pasta Store.Tests e executar o seguinte comando

```bash
  dotnet test
```


## Autores

- [Davi Francisco](https://github.com/KingOfTheHunt)

