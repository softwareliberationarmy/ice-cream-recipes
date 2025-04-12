# C# and dotnet coding standards

## General

- follow coding best practices (e.g. SOLID principles, DRY)
- avoid duplication where possible. If you see duplication in your design, try to extract the duplicated logic out to a separate class that's consumed in multiple places.
- use test-driven development whenever possible. Write a failing unit test that tests the behavior you're about to implement, run the test and watch it fail, make sure it fails for the right reason, then write just enough code to get the test to pass. Afterwards, refactor the code to remove duplication.
- use dependency injection and interfaces to create a loosely coupled and highly testable design.

## Unit testing

- use the xunit testing framework
- use AutoMocker and Moq to create the 'class under test' and mock out necessary dependencies
- use nested test classes to separate different test scenarios from testing the same class
- use FluentAssertions for the assert part of each unit test
