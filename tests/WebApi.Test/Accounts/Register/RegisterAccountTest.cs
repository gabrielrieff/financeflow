//using commonTestUtilities.Requests.Account;
//using FluentAssertions;
//using System.Net;
//using System.Text.Json;

//namespace WebApi.Test.Accounts.Register;

//public class RegisterAccountTest : FinanceFlowClassFixture
//{
//    //private const string Method = "api/Account";

//    //private readonly string _token;

//    //public RegisterAccountTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
//    //{
//    //    _token = webApplicationFactory._userIdentity.GetToken();
//    //}

//    //[Fact]
//    //public async Task Success()
//    //{
//    //    var request = RequestAccountJsonBuilder.Build();

//    //    var result = await DoPost(Method, request, _token);

//    //    result.StatusCode.Should().Be(HttpStatusCode.Created);

//    //    var body = await result.Content.ReadAsStreamAsync();

//    //    var response = await JsonDocument.ParseAsync(body);

//    //    response.RootElement.GetString().Should().Be("Registro concluído");
//    //}
//}
