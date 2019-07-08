Feature: CalculatingForecast
	In order to create an exact plan that we can follow
	As someone who doesn't understand agile
	I want to be able to calculate everything upfront

Scenario: Committing less then normal to account for bugs
 Given a history of sprints
   And production issues have occured in the past
  When we make a sprint forecast
  Then commit less then we normally would to account for disaster to strike

 