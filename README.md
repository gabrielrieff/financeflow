## Sobre

Esta é uma API desenvolvida utilizando **.NET 8**, com princípios do **Domain-Driven Design (DDD)** para fornecer uma solução estruturada e eficaz no gerenciamento de despesas pessoais. O principal objetivo é permitir ao usuário registrar suas despesas, detalhando algumas informações essenciais como título, data descrição, valor e forma de pagamento, utilizando uma forma segura de armazenar os dados, com o banco de dados **MySQL**.

A arquitetura da API segue em REST, utilizando métodos HTTP padrão para uma comunicação eficiente e simplificada.

Dentre os pacotes NuGet utilizados, o AutoMapper é responsável pelo mapeamento  entre objetos de domínio e requisição/resposta, reduzindo a necessidade de código repetitivo e manual. O FluentAssertivo é utilizado nos testes de unidade para tornar as verificações mais legíveis, ajudando a escrever testes claros e compreensíveis. Para as validações, o FluentValidation é usado para implementar regras de validação de formas simples e intuitivas nas classes de requisições, mantendo o código limpo e fácil de manter. Por fim, o EntityFramework atua como ORM que simplifica as interações com o banco de dados, permitindo o uso de objetos .NET para manipular dados diretamente, sem a necessidade de lidar com consultas SQL.

![home-image]

### Features

- **Domain-Driven Designer (DDD)**: Estrutura modular que facilita o entendimento e a manutenção do domínio da aplicação.
- **Testes de unidade**: Testes abrangentes com FluentAssention para garantir a funcionalidade e a qualidade.
- **Geração de Relatórios**: Capacidade de exportar documentos detalhados em PDF e Excel, oferecendo uma análise visual e eficaz das despesas.
- **RESTful API com documentação Swagger**: Interface documentada que facilita a interação e o teste por parte dos desenvolvedores.

### Constrido com

![badge-dot-net]
![badge-windows]
![badge-visual-studio]
![badge-mysql]
![badge-swagger]

## Getting started

para obter uma copia local funcionando, siga estes passos simples.

### Requisitos

* Visual Studio versão 2022+ ou Visual studio code
* Windowns 10+ ou Linux/MecOS com [.NET SDK][dot-net-skd] instalado
* MySql

### Instação

1. Clone o repositorio: 

    ```sh
    git clone https://github.com/gabrielrieff/financeflow.git
    ```

2. Preencha as informações no arquivo `appsettings.Develepment.json`.
3. Execute a API e aproveite.





<!-- Links -->
[dot-net-skd]: https://dotnet.microsoft.com/pt-br/download/dotnet/8.0

<!-- images -->
[home-image]: imagens/heroimage.png

<!-- Badges -->
[badge-dot-net]: https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=fff&style=for-the-badge
[badge-windows]:https://img.shields.io/badge/windows-blue?logo=windows&logoColor=white&style=for-the-badge
[badge-visual-studio]: https://img.shields.io/badge/Visual%20Studio-5C2D91?logo=visualstudio&logoColor=fff&style=for-the-badge
[badge-mysql]: https://img.shields.io/badge/MySQL-4479A1?logo=mysql&logoColor=fff&style=for-the-badge
[badge-swagger]: https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=000&style=for-the-badge