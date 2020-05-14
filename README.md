# Purpose
This package has been created to standardize communication way between ASP.NET Core WebAPI and SPA clients like Vue, React or Angular.

**How does it work**
During processing your request you are able to store all importnat informations about reuest processing result. IReaponseCreator generates unified response metadata that contains serveral properties like:

    ValidationResults - Any errors about sent input to the server. If input 
    				    (from request) is invalid this object will contain 
    				    all validation information.
    Messages - Business messages that are catched during request processing
    TimeStamp - Response creation time
    Status - Clear response with request status
    Data - Action response for futher processing at the client side.
    
And then it will be passed to client.
    
   **How to use**

1. your ExampleDTO should inherit after IInputDTO.

	    CompanyDTO: IInputDTO

2. Register dependency in your DI container.
	 Use IResponseCreator as ResponseCreator
3. Inject IResponseCreator into your ApplicationService

	    public CompanyService(IResponseCreator responseCreator)
	    {
	    	this._responseCreator = responseCreator
	    }
4. Validate your input in your service method or wherever you want

	    public  NewCompanyDTO  CreateNewCompany(CompanyDTO  input) 
	    {
		    input.ValidateInput(this._responseCreator);

		    if(!this._responseCreator.IsValid())
		    {
			    // write some errors to response creator using
			    this._responseCreator.AddMessage(...)
		    }
		    
		    // the rest of code if input is fine
	    }

5. To process validations use InputValidator inside method ValidateInput from your InputDTO class.

	   var iv = new InputValidator<CompanyDTO>(this, responseCreator);

       iv.ForString(x => x.Name)
           .MinLength(1)
           .MaxLength(100);

       iv.ForString(x => x.TaxNumber)
           .MinLength(10)
           .MaxLength(10);
6. To create response just use IResponseCreator in you Controller action method like this.

   		[HttpPost]
	    [Route("new")]
	    public IActionResult CreateNewCompany(CompanyDTO input)
	    {
		     NewCompanyDTO responseData = this.companyService.CreateNewCompany(input);
            if (this._responseCreator.IsValid())
            {
                return Ok(this._responseCreator.CreteResponse(responseData));
            }
            else
            {
                return BadRequest(this._responseCreator.CreateResponseWithNoData());
            }
        }
