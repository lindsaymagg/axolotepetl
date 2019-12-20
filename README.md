# axolotepetl
Videojuego hecho con Unity 3D / Video game made in Unity 3D


Menus
MainMenu: 
Controlar el menú principal, y los métodos de los botones. Cargar la próxima escena. Controls the main menu. Controls functions of the buttons. Loads the next scene.
Play
NewGame
Continue
ReadInstructions
NextPage
PreviousPage
ReadCredits
GoBackToMenu
Quit
MapScreenManager:
Escoger chef si se inicia el juego. Mostrar niveles. Activar botones de los niveles que hayan sido desbloqueados.
Choose chef if starting the game. Show levels. Activate buttons of levels that have 
been unlocked.
SelectChefMujer
SelectChefHombre
SelectLevel

Chef Scripts
  Chef
  Controlar movimiento de un solo lado del chef y sus animaciones de caminar. Inicializar configuraciones de nivel (estado = cocinando, número de clientes alimentados = 0, número de clientes desmayados = 0), y cual de los chefs para usar. Abrir el refri. Detectar para ver si el chef está en frente de una estación. Jugar minijuego si está en frente de una estación y si lleva un ingrediente crudo. Poner ingrediente preparado en emplatado. Jugar emplatado. Tirar ingrediente o platillo. Cocinar ingrediente después de jugar un minijuego exitosamente. Servir platillo a cliente si hay un cliente en el mismo lado como el chef que pidió ese platillo. Alimentar al Axolote si está en el nivel 5 y si está en el mismo lado como el chef. Cambiar estados del chef.
Controls same-size movement of chef and walking animations. Initializes settings of 
level (state = cooking, number of fed clients = 0, number of fainted clientes = 0, 
which chef to use. Opening of refrigerator. Detecting to see if chef is in front of a 
station. Play minigame if in front of station and holding raw ingredient. Put prepared 
ingrediente at plating station. Play plating minigame. Throw away ingredient. Cook 
ingredient after successfully playing minigame at correct station. Deliver meal to 
client if client on same side as chef ordered meal held. Feed Axolote if on level 5 and 
on correct side of kitchen. Controls changes of states of chef from cooking to serving 
and vice versa.
Init
Move
MoveDeliver
OnTriggerEnter
OnTriggerExit
Cook
PutIngredientOnPlate
GetPlate
Feed
	Notes:
ChefState is type enum { COOKING, DELIVERING }
Activate minigames using sphere colliders with triggers and OnTriggerEnter
enPlatos, enPicar, enEstufa etc. are all booleans that are true only if player has entered the collider. Return false with OnTriggerExit, when player walks away
To feed, get array of all client game objects. Iterate through array, only check clients which are on the same side as the chef (check client.sideID). For all clients with same side ID as the chef, see if either their first or their second meal is the meal being delivered. If so, make it null, and stop checking the other clients.
ChefMovement
Responsable para mover el chef de un lado de la cocina al otro. Checar si un ingrediente fue preparado en la estación correcto.
Responsible for moving the chef from one side of the kitchen to another. Checks to see if the ingredient was prepared in the correct station.
UpdateStation
MoveLeft
MoveRight
Notes:
Waypoints are used for smooth movements when turning to another side of the kitchen
IngredientUI
Crear una imagen de un ingrediente o un platillo que flota sobre la cabeza del chef.
Creates an image an ingredient or a dish to float over the head of the chef.

Clientes / Clients
Client 
Clase de cliente. Cada cliente tiene: un enum estado (hambriente o satisfecho), un int sideID que corresponde con el lado del cliente, un int ID para identificar cada cliente, un int numMeals que describe el numero de platillos pedidos, dos Meals (platillos) meal y secondMeal (secondMeal puede ser null) que representan los platillos pedidos por este cliente. Sus platillos pedidos se asignan en esta clase, y aquí también se cambia el estado.
Client class. Every client has an enum state (hungry or full), an int sideID that corresponds to the side of the client, an int ID to identify each client, an int numMeals that describes the number of dishes ordered, and two Meals (dishes) called meal and secondMeal that represent the dishes ordered. secondMeal can be null. A client's ordered dishes are assigned in this class, and their state changes in this class.
GroupSpawner
SpawnPoints

Minijuego / Minigames
CounterPicar
GanaEmplatado
GHBoton
JuegaEstufa
JuegaEmplatado
JuegaLicuadora
JuegaMezclar
JuegaMolcajete
JuegaPicar
JuegaRallador
MashingTime
MueveCuchillo
mueveFlechaRallar
Rotacion

