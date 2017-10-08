# MultiCreditCard

Projeto desenvolvido como solução do teste da Stone

O projeto MultiCreditCard utiliza as seguintes tecnologias.

* ASP.NET Core 2.0 (https://www.microsoft.com/net/download/core)
* [Log4Net] para geração de logs (https://github.com/apache/logging-log4net/)
* [Fluent Validation] para utitizar o padrão fail-fast (https://github.com/JeremySkinner/FluentValidation)
* [MediatR] com a bibilioteca (https://github.com/jbogard/MediatR)
* [PaperTrail] para visualização dos logs (https://papertrailapp.com) 
* [SQL Server] Banco de dados.
* [JWT] para autenticação de usuário baseada em token (https://tools.ietf.org/html/rfc7519)


## Passos para executar para rodar a aplicação

* Instalar o **.Net Core 2.0 SDK** (https://www.microsoft.com/net/download/core)
* Clonar o projeto (https://github.com/ddrsdiego/multi-credit-card.git)
* Criar um banco de dados SQL Server utilizando o script na pasta **multi-credit-card/ScriptsTables.sql**
* Informar a connectionString no arquivo de configuração **multi-credit-card/src/MultiCreditCard.Api/appsettings.json**
```
  "ConnectionStrings": {
    "MultCreditCard": "Server=<servidor>;Database=<data base name>;Trusted_Connection=True;"
  },
```  
* cd multi-credit-card\src\MultiCreditCard.Api
* dotnet run --project .\MultiCreditCard.Api.csproj

## Endpoints

### Autentica usuário
| Campo     | Tipo     |Descrição
| --------- | -------- |------------- |
| email     | String   | Email do usuário
| password  | String   | Senha do usuário

```
### POST
http://localhost:60091/api/authenticate
{
	"email":"lararibeirocavalcanti@jourrapide.com",
	"password":"1234mudar"
}
```

### Registrar um novo usuário
| Campo          | Tipo   |Descrição
| -------------  | ------ |------------- |
| Content-Type   | String |**application/json**

| Campo           | Tipo          |Descrição
| -------------   | ------------- |------------- |
| userName        | String        | Nome do usuário
| documentNumber  | decimal       | Número do documento do usuário
| email           | String        | Email do usuário
| password        | String        | Senha do usuário

```
### POST
http://localhost:60091/api/users/
{
	"userName":"Lara Ribeiro Cavalcanti",
	"documentNumber":10883620448,
	"email":"lararibeirocavalcanti@jourrapide.com",
	"password":"1234mudar"
}
```

### Consultar dados da Carteira.
| Campo          | Tipo   |Descrição
| -------------  | ------ |------------- |
| Authorization  | String |**Informar como "Bearer Token"**

```
### GET
http://localhost:60091/api/users/credit-cards
```

```
### Respose
{
    "userId": "7b5c4c7e-32db-4a47-94c8-e65fa1654c3b",
    "creditLimitWallet": 5270,
    "dataTimeQuery": "2017-10-08T10:35:07.773519-03:00",
    "creditCards": [
        {
            "creditCardNumber": 5212410848753186,
            "creditCardType": "Mastercard",
            "creditLimit": 3520,
            "payDay": 5,
            "expirationDate": "06/19"
        },
        {
            "creditCardNumber": 5350172366189464,
            "creditCardType": "Mastercard",
            "creditLimit": 1750,
            "payDay": 15,
            "expirationDate": "02/19"
        }
    ]
}
```

### Adicionar cartões de crédito a carteira do cliente

| Campo          | Tipo   |Descrição
| -------------  | ------ |------------- |
| Authorization  | String |**Informar como "Bearer Token"**
| Content-Type   | String |**application/json**

| Campo            | Tipo     |Descrição
| -------------    | ---------|------------- |
| creditCardType   | string   | Bandeira do Cartão ( 1 = Visa, 2 = Master Card, 3 = AmericanExpress )
| creditCardNumber | decimal  | Número do Cartão de Crédito
| cvv              | string   | Código de Segurança
| expirationDate   | string   | Data de Expiração do Cartão de Crédito
| printedName      | string   | Nome Impresso no Cartão de Crédito
| payDay           | string   | Dia do pagamento da fatura do Cartão de Crédito
| creditLimit      | decimal  | Valor do Limite de Crédito do Cartão

```
### POST
http://localhost:60091/api/credit-cards
{
	"creditCardType":2,
	"creditCardNumber":5350172366189464,
	"cvv":"746",	
	"expirationDate":"02/19",	
	"printedName":"LARA CAVALCANTI",
	"payDay":15,
	"creditLimit":1750
}
```
### Requisitar compras.

| Campo          | Tipo   |Descrição
| -------------  | ------ |------------- |
| Authorization  | String |**Informar como "Bearer Token"**
| Content-Type   | String |**application/json**

| Campo            | Tipo     |Descrição
| -------------    | ---------|------------- |
| amountValue      | decimal  | Valor da Compra a ser executada

```
### POST
http://localhost:60091/api/wallets/buy
{
	"amountValue":1450
}
```