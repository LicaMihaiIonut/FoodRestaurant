## EF Migrations:
dotnet ef migrations add Initial --project FoodRestaurant.Repositories --startup-project FoodRestaurant --context FoodRestaurantContext
dotnet ef database update --project FoodRestaurant.Repositories --startup-project FoodRestaurant --context FoodRestaurantContext