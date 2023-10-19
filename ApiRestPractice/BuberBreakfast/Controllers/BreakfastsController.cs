using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Contracts.BuberBreakfast;
using BuberBreakfast.Models;
using BuberBreakfast.ServiceErrors;
using BuberBreakfast.Services.Breakfasts;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers; 



public class BreakfastsController : ApiController {
    private  readonly IBreakfastService _breakfastService;

    public BreakfastsController(IBreakfastService breakfastService)
    {
        _breakfastService = breakfastService;
    }

    [HttpPost]

    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {
        var breakfast = new Breakfast(
            Guid.NewGuid(),
            request.Name,
            request.Description,
            request.StarDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet);

        ErrorOr<Created> createBreakfastResult = _breakfastService.CreateBreakfast(breakfast);

       return createBreakfastResult.Match(
        created => CreatedAtGetBreakfast(breakfast),
        errors => Problem(errors)
       );


        //TODO : save breakfast to database 

    }

   

    private static BreakfastResponse MapBreakfastRespone(Breakfast breakfast)
    {
        return new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.Savory,
            breakfast.Sweet
        );
    }

    [HttpGet("{id:guid}")]

    public IActionResult GetBreakfast(Guid id)
    {
        ErrorOr<Breakfast> getbreakfastResult = _breakfastService.GetBreakfast(id);

        return getbreakfastResult.Match(breakfast => Ok(MapBreakfastRespone(breakfast)),
        errors => Problem(errors));

      //  if (getbreakfastResult.IsError && getbreakfastResult.FirstError == Errors.Breakfast.NotFound)
      //  {
      //      return NotFound();
      //  }

      //  var breakfast = getbreakfastResult.Value;

        //BreakfastResponse response = MapBreakfastRespone(breakfast);
//
        //return Ok(response);
    }

   

    [HttpPut("{id:guid}")]

    public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
    {

        var breakfast = new Breakfast(
            id, 
            request.Name,
            request.Description,
            request.StarDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet
        );

        ErrorOr<UpsertedBreakfast> upsertedBreakfastResult =   _breakfastService.UpsertBreakfast(breakfast);   

        // TODO: return 201 if a new Breakfast was created
        return upsertedBreakfastResult.Match(
            upserted => upserted.IsNewlyCreated ? CreatedAtGetBreakfast(breakfast) : NoContent(),
            errors => Problem(errors)
        );
    }

    
    [HttpDelete("{id:guid}")]
      public IActionResult DeleteBreakfast(Guid id)
    {

        ErrorOr<Deleted> deleteBreakfastResult = _breakfastService.DeleteBreakfast(id);

        return deleteBreakfastResult.Match(
            deleted => NoContent(),
            errors => Problem(errors)
        );
        
    }

     private CreatedAtActionResult CreatedAtGetBreakfast(Breakfast breakfast)
    {
        return CreatedAtAction(
                actionName: nameof(GetBreakfast),
                routeValues: new { id = breakfast.Id },
                value: MapBreakfastRespone(breakfast));
    }

}