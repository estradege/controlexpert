# controlexpert
Schneider Electric Control Expert Toolkit

## 1) Quick start
```c#
var xef = new XefReader();
await xef.LoadZefAsync("Stu/m340.zef");

var contentHeader = await xef.GetContentHeaderAsync();
var variables = await xef.GetVariablesAsync();
var program = await xef.FindFirstOrDefaultAsync("Motor1");

// and more...
```
