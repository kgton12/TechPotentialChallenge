
# Instruções para o Teste de Tecnologia

1. Crie um fork deste projeto [aqui](https://gitlab.com/Pottencial/tech-test-payment-api/-/forks/new). É preciso estar logado na sua conta Gitlab.
2. Adicione @Pottencial (Pottencial Seguradora) como membro do seu fork. Você pode fazer isto em: `https://gitlab.com/your-user/tech-test-payment-api/settings/members`.
3. Quando você começar, faça um commit vazio com a mensagem "Iniciando o teste de tecnologia" e quando terminar, faça o commit com a mensagem "Finalizado o teste de tecnologia".
4. Commit após cada ciclo de refatoração pelo menos.
5. Não use branches.
6. Você deve prover evidências suficientes de que sua solução está completa indicando, no mínimo, que ela funciona.

## O Teste

Construir uma API REST utilizando .Net Core, Java ou NodeJs (com Typescript).

- A API deve expor uma rota com documentação swagger (http://.../api-docs).
- A API deve possuir 3 operações:
    - **Registrar venda**: Recebe os dados do vendedor + itens vendidos. Registra venda com status "Aguardando pagamento".
    - **Buscar venda**: Busca pelo Id da venda.
    - **Atualizar venda**: Permite que seja atualizado o status da venda.

### Possíveis status:
- Pagamento aprovado
- Enviado para transportadora
- Entregue
- Cancelada

Uma venda contém informação sobre o vendedor que a efetivou, data, identificador do pedido e os itens que foram vendidos.

O vendedor deve possuir id, cpf, nome, e-mail e telefone.

A inclusão de uma venda deve possuir pelo menos 1 item.

A atualização de status deve permitir somente as seguintes transições:
- De: Aguardando pagamento  Para: Pagamento Aprovado
- De: Aguardando pagamento  Para: Cancelada
- De: Pagamento Aprovado  Para: Enviado para Transportadora
- De: Pagamento Aprovado  Para: Cancelada
- De: Enviado para Transportadora  Para: Entregue

A API não precisa ter mecanismos de autenticação/autorização.

A aplicação não precisa implementar os mecanismos de persistência em um banco de dados, eles podem ser persistidos "em memória".

## Pontos que Serão Avaliados

- Arquitetura da aplicação - embora não existam muitos requisitos de negócio, iremos avaliar como o projeto foi estruturado, bem como camadas e suas responsabilidades.
- Programação orientada a objetos.
- Boas práticas e princípios como SOLID, DDD (opcional), DRY, KISS.
- Testes unitários.
- Uso correto do padrão REST.


# TechPotentialChallenge

## Descrição

Este projeto é uma API para gerenciar pedidos, permitindo a atualização de status dos pedidos conforme as transições permitidas.

## Estrutura do Projeto

- **TechPotentialChallenge.API**: Contém a API principal.
- **TechPotentialChallenge.Communication**: Contém as classes de comunicação, como requests e responses.
- **TechPotentialChallenge.Domain**: Contém as entidades e regras de negócio.
- **TechPotentialChallenge.Infrastructure**: Contém a configuração do banco de dados e a persistência dos dados.

## Requisitos

- .NET 8.0
- SQLite

## Configuração

### Banco de Dados

A aplicação utiliza um banco de dados SQLite. A string de conexão pode ser configurada nos arquivos `appsettings.json` e `appsettings.Development.json` na seção `ConnectionStrings`.

### Executando a Aplicação

1. Clone o repositório:
    ```sh
    git clone <URL_DO_REPOSITORIO>
    ```
2. Navegue até o diretório do projeto:
    ```sh
    cd TechPotentialChallenge
    ```
3. Restaure as dependências:
    ```sh
    dotnet restore
    ```
4. Execute a aplicação:
    ```sh
    dotnet run --project TechPotentialChallenge.API
    ```

A aplicação estará disponível em `http://localhost:5281`.

## Atualização de Status

A atualização de status deve permitir somente as seguintes transições:
- De: Aguardando pagamento  Para: Pagamento Aprovado
- De: Aguardando pagamento  Para: Cancelada
- De: Pagamento Aprovado  Para: Enviado para Transportadora
- De: Pagamento Aprovado  Para: Cancelada
- De: Enviado para Transportadora  Para: Entregue

## Pontos que Serão Avaliados

- Arquitetura da aplicação - embora não existam muitos requisitos de negócio, iremos avaliar como o projeto foi estruturado, bem como camadas e suas responsabilidades.
- Programação orientada a objetos.
- Boas práticas e princípios como SOLID, DDD (opcional), DRY, KISS.
- Testes unitários.
- Uso correto do padrão REST.

## Testes

Para executar os testes unitários, utilize o comando:
```sh
dotnet test