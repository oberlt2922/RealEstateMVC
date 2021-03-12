# RealEstateMVC
A real estate website using ASP.NET Core MVC and Entity Framework Core


This website has a home page with a select element that displays houses depending on the county that is selected.
When the user hovers the mouse over an image, a slideshow starts.  When the mouse moves away from the slideshow, the slideshow stops.  I used javascript for this.
When the use clicks on a house they are brought to a details page that has slideshow with right and left buttons, the buttons use javascript to change the visible image.
This page also displays the rest of the house's information.

The admin page also has a select element that displays houses in a table based on the county selected.
The admin can select a house or create a new house.
Selecting a house displays a similar page to the details page except it also has an update and delete button.
when the update button is clicked, the admin is redirected to a page that has text boxes, an file input control, and a button to delete the visible image in the slideshow.
I used a smartystreets API on the create and edit paged to ensure that the entered address, city, state, and zip code belong to a USPS-verified address.

The schedule page shows a list of houses in a table.
When a house is selected the user can schedule a viewing of the house by entering their information.

I am currently working to add more features to the site and make the design more practical.

I want to add a login feature
When a user views a house from the home page, ther will be a link so they can schedule a viewing of the house.  This way they do not have to go to the schedule page and 
search for houses by county.
When an admin views a house from the home page, the update and delete buttons will be displayed.

The error message that is displayed when an address is invalid is crude and I want to make it more visually appealing and change where it appears.

I also would like to use some kind of map API so that users can see the hosues location on a map.

I plan to add the database to AWS

I still have a lot of work to do when it comes to styling.


Currently every database call has it's own method to avoid repeating C# code, i also used sublayouts with @rendersections so i could avoid repeating HTML code
