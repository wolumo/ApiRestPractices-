namespace BuberBreakfast.Contracts.Breakfast; 

public record UpsertBreakfastRequest(
   string Name, 
   string Description, 
   DateTime StarDateTime, 
   DateTime EndDateTime, 
   List<string> Savory, 
   List<string> Sweet 
);