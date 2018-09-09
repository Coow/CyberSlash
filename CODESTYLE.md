# Code Style Reference File

## 1. Purpose
Coding conventions serve the following purposes:
* They create a consistent look to the code, so that readers can focus on content, not layout.
* They enable readers to understand the code more quickly by making assumptions based on previous experience.
* They facilitate copying, changing, and maintaining the code.

## 2. Naming Conventions
* In short examples that do not include using directives, use namespace qualifications. If you know that a namespace is imported by default in a project, you do not have to fully qualify the names from that namespace. Qualified names can be broken after a dot (.) if they are too long for a single line, as shown in the following example.
```
var currentPerformanceCounterCategory = new System.Diagnostics.PerformanceCounterCategory();
```
* You do not have to change the names of objects that were created by using the Visual Studio designer tools to make them fit other guidelines.

## 3. Layout Conventions
* Write only one statement per line.
* Write only one declaration per line.
* Indentation tabs are four spaces wide.
* If if continuation lines are not automatically indented, indent them one tab.
* Add no less and no more than one blank line between method definitions and property definitions.
* Use parentheses to make clauses in an expression apparent, either:
```csharp
if ((val1 > val2) && (val1 > val3))
{
    // Take appropriate action.
}
```
or
```csharp
if ((val1 > val2) && (val1 > val3)) {
    // Take appropriate action.
}
```
but not
```csharp
if ((val1 > val2) && (val1 > val3))
    // Take appropriate action.
```
or
```csharp
if ((val1 > val2) && (val1 > val3)) // Take appropriate action.
```

## 4. Commenting Convetions
* Place the comment on a separate line, not at the end of a line of code.
* Begin comment text with an uppercase letter.
* End comment text with a period/full stop.
* Insert one space between the comment delimiter and the comment text.
* Do not create formatted blocks of asterisks around comments.

## 5. Language Guidelines
### Strings
* Use string interpolation to concatenate short strings.
* To append strings in loops, use a StringBuilder object.

### Local Variables
* Use implicit typing (var) for local variables when the type of the variable is obvious from the right side of the assignment or when the precise type is not important.
```csharp
// Here these three variables have obvious types.
var var1 = "This is clearly a string.";
var var2 = 42;
var var3 = Convert.ToInt32(Console.ReadLine());
```
* Do not use implicit typing (var) when the type is not apparent from the right side of the assignment.
```csharp
// Using var here wouldn't indicate that ExampleClass.HowOldIsThisCow() would return a type Integer.
int results = ExampleClass.HowOldIsThisCow();
```
* Do not rely on the variable name to specify the type of the variable, it might not be correct.
```csharp
// What if the user uses a letter as input? Then the name is incorrect.
var inputInt = Console.ReadLine();
```
* Avoid the use of `var` in place of `dynamic`.
* Use implicit typing to determine the type of the loop variable in `for` and `foreach` loops.

### Unsigned Data Types
* In general, use `int` rather than unsigned types. The use of `int` is easier to use for interactions with other libraries.

### Exception Handling
* Use try-catch statements for most exception handling.
* Simplify your code by using the C# `using` statement.

### And and Or Operators
* To avoid exceptions and increase performance, use `&&` and `||` instead of `&` and `|`.

### New Operators
* Use the conside form of object instantiation with implicit typing.
```csharp
ExampleClass instance = new ExampleClass();
// Rather than.
var instance = new ExampleClass();
```
* Use object initializers to simplify object creation.
```csharp
var instance = new ExampleClass { Name = "HackNSlash", ID = 1337, Location = "Farm", Age = 20.3 };

// Rather than.
var instance = new ExampleClass();
instance.Name = "HackNSlash";
instance.ID = 1337;
instance.Location = "Farm";
instance.Age = 20.3;
```
### Event Handling (Not mandatory)
* Try to use lambda expressions if you are defining an event handler that you do not need to remove later.

### Static Members
* Call static members by using the class name: `ClassName.StaticMember`. This practice makes code more readable by making static access clear. Do not qualify a static member defined in a base class with the name of a derived class. While that code compiles, the code readability is misleading, and the code may break in the future if you add a static member with the same name to the derived class.

# ------------------------------------------------------------
# Thank you for taking the time to read this! <3
# ------------------------------------------------------------