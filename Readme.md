# CoffeeMachine

To control Coffee Machine via editor window choose in upper bar CoffeeMachine => CoffeMachineWindow  
To control it by UI just enter playmode.  

In playmode please use 16:9 aspect ration, UI wasn't optimalized and tested in different ratio  
  
Coffee machine state is stored in scriptable object, and because of that state is stored beetwen sesions, and this state is shared between playmode and editor. Of course this approuch wouldn't work in standalone build because in standalone build changes in scriptable objects aren't saved. One of possible  approach to be abel to save coffee machine state in standalone would be to to create memento struct with all required fileds: `List<CoffeMachineSettings.CoffeeSettings> favorites coffes `, `float amountOfWater`, `float amountOfCoffee`, `flaot amountOfCoffeGrounds` and `float amountOfWaterInTray`, when exiting game coffee machine state stroed in SO would be transfered to this memento struct, and then seiralized to file via e.g JSON. At entering playmode ths file would be deserialized and field values would be loaded to coffee machine stated used by rest of the game logic.