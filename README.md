# reCaptcha-V3

reCAPTCHA V3 for .NET 5.

## Usage

1. Install the package.
```sh
Install-Package reCAPTCHA-v3
```

2. Get your `Site-Key` and `Secret-Key` to use **reCAPTCHA** from [here](https://www.google.com/recaptcha/admin/create).

3. Add following service registrations to `ConfigureServices` method in **Startup.cs**.
```csharp
services.AddRecaptcha(recaptchaOptions =>
{
  recaptchaOptions.SecretKey = "your_recaptcha_secret_key";
  recaptchaOptions.SiteKey = "your_recaptcha_site_key";
});
```
4. Specify reCAPTCHA tag helper in `_ViewImports.cshtml` as below.
```cshtml
//...
@addTagHelper *, ReCaptcha
```

5. Use the `<recaptcha />` tag helper in your **View**. (e.g. Home/Index.cshtml)
```cshtml
<h5 class="display-5">Recaptcha MVC Demo</h5>
<div class="container">
    <form id="my-form" method="post" asp-action="Index">
        <div class="form-group">
            <label>Username</label>
            <input type="text" asp-for="Username"/>
        </div>
        <div class="form-group">
            <label>Password</label>
            <input type="text" asp-for="Password"/>
        </div>
        <input type="hidden" asp-for="RecaptchaToken" id="recaptcha-token" />
        
        <div class="form-group">
            <button class="btn btn-dark">
                Submit
            </button>
        </div>
    </form>
</div>

<recaptcha action-name="login" execute-method="tokenProvider"/>

<script>
    function tokenProvider(token) {
        document.getElementById("recaptcha-token").value = token;
    }
  </script>
```

6. After submitting the form and sending a **POST** request to the server, you can simply check the user is allowed to do the action or not.
```csharp
[HttpPost]
public async Task<IActionResult> Index(RecaptchaModel model)
{
  var recaptcha = await _recaptchaVerifier.VerifyAsync(model.RecaptchaToken);

  if (recaptcha.Success && recaptcha.Score >= 0.7)
  {
    return Ok(recaptcha);
  }

  return BadRequest(new {message = "You can not proceed this action "});
}
```
