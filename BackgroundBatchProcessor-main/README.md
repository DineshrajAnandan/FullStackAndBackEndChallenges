# Background Batch Processor

## Problem Statement


### Web App
- The application will require the user to enter two numbers between 1 and 10
	- The first number, X, is how many batches of numbers should be processed
	- The second, Y, is how many numbers will be processed per batch
- A start button will be available to click once the input is ready
- Once started, the application will trigger back-end work
- A grid of the batches, their remaining numbers to process, and their current totals should be displayed
	- The grid should update every 2 seconds. Use polling through Angular and not through a third-partyclibrary like SignalR
- A grand total (sum of all batch totals) should be displayed
- Once all batches are processed, the user can start another batch, clearing previous results
- Persist these results, using Entity Framework and allow the user to retrieve and view the last batch on a separate page in a grid

### Web API
- An endpoint will exist to start processing X batches with Y numbers per batch managed by a Processor
- The Processor will contain two workers to manage the processing by listening to events raised from its workers: Generator Manager, MultiplierManager
- each batch, the Processor will ask the Generator Manager to begin generating Y random integers between 1 and 100
	- For each number, a random delay of 5 to 10 seconds should be used to simulate work
	- When the GeneratorManager generates a number, it will raise an event for the Processor identifying the batch and number
- For each generated number in a batch, the Processor will ask the Multiplier Manager to multiply the number by 2, 3, or 4 (chosen at random)
	- A random delay of 5 to 10 seconds should be used to simulate work
	- When the Multiplier Manager multiplies a number, it will raise an event for the Processor identifying the batch and number
- The Processor will take each multiplied number for a batch and aggregate them as received
- An endpoint will exist for retrieving the current processing state
