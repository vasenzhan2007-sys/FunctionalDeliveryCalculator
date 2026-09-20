# Functional Delivery Calculator

Yerkezhan Vassenzhan | IT-2504

## Description

This is a console-based delivery cost calculator written in C#.

The program asks the user for:
- Base delivery price
- Number of items
- Express delivery status
- Delivery type
- Delivery zone

The program validates the input and calculates the final delivery price using several pricing rules.

## Delivery Types

- Pickup: -20%
- Courier: 0%
- DoorToDoor: +15%

## Delivery Zones

- City: 0%
- OutsideCity: +25%
- Remote: 0%

## Item Rules

- 1–3 items: 0%
- 4–7 items: +10%
- 8 or more items: +20%

## Express Delivery

- false: 0%
- true: +30%

The rules are applied in this order:

1. Base price
2. Number of items
3. Delivery type
4. Delivery zone
5. Express delivery
6. Final rounding to 2 decimal places

## Input and Output

All console input and output is handled in the `Main()` method.

The program uses `TryParse` methods to safely convert user input and avoid exceptions for expected invalid input.

## Calculation Functions

The calculation logic is separated from console operations.

The main calculation functions are:

- `CalculateDeliveryPrice()`
- `ApplyItemsRule()`
- `ApplyDeliveryTypeRule()`
- `ApplyDeliveryZoneRule()`
- `ApplyExpressRule()`

These functions only calculate values and do not use `Console.ReadLine()` or `Console.WriteLine()`.

## Use of Func

The program uses `Func<decimal, decimal>` to represent pricing rules as functions.

For example:

```csharp
Func<decimal, decimal> itemRule =
    currentPrice => ApplyItemsRule(currentPrice, items);
```
The `ApplyRule()` function receives another function as a parameter:

```csharp
static decimal ApplyRule(
    decimal price,
    Func<decimal, decimal> rule) =>
    rule(price);
```

This allows different pricing rules to be passed to the same function.

## Why TryParse is Useful

`TryParse` safely converts strings into numeric or boolean values.

For example:
```csharp
decimal.TryParse()
int.TryParse()
bool.TryParse()
Enum.TryParse()
```
If the input is invalid, the program displays an error message and stops without an unhandled exception.

## Test Cases

| # | Test | Expected Result | Actual Result |
|---|---|---:|---:|
| 1 | 1000, 2 items, false, Courier, City | 1000.00 | 1000.00 |
| 2 | 1000, 5 items, false, Courier, City | 1100.00 | 1100.00 |
| 3 | 1000, 8 items, false, Courier, City | 1200.00 | 1200.00 |
| 4 | 1000, 2 items, false, Pickup, City | 800.00 | 800.00 |
| 5 | 1000, 2 items, false, DoorToDoor, OutsideCity | 1437.50 | 1437.50 |
| 6 | 1000, 2 items, true, Courier, City | 1300.00 | 1300.00 |
| 7 | Invalid price: `abc` | Error message | Passed |
| 8 | Negative price: `-100` | Error message | Passed |
| 9 | Invalid delivery type: `Pizza` | Error message | Passed |

## Conclusion

This project demonstrates safe input conversion, control flow, functions, lambdas, `Func`, and separation of input/output from calculation logic.