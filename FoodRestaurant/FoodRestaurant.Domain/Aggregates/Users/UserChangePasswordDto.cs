﻿namespace FoodRestaurant.Domain.Aggregates.Users;

public class UserChangePasswordDto
{
    public string Email { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}
