# _{Hair-Salon}_

#### _{User is able to add stylists to a database. User is also able to add clients for an individual stylist.}, {2/16/17}_

#### By _**{Alexander Neumann @ Epicodus}**_

## Description

_{This websites allows a user to make an appointment with a specific stylist. As well as allowing a user to create new stylists.}_

## Setup/Installation Requirements

* _Clone from Github_
* _Open phpMyAdmin, import SQL files in Hair-Salon folder._
* _While in the cloned project path execute dotnet run_


## Known Bugs

_{None currently known of.}_

## Support and contact details

_{alexander.daniel.neumann@gmail.com}_

## Technologies Used

_{HTML, CSS, C#, ASP.NET MVC 1.1.3,Unit Testing, MAMP, SQL, MyPhpAdmin}_

## _{Specifications}_
## Class Name: Stylist
_{Properties: Name, Id }_
_{Methods: Getters, Setters, Save(), GetAll(), Find(int id), FindClients(), DeleteAll(), DeleteRow(int id), Override Equals(System.Object), GetHashCode() }_

## Class Name: Client
_{Properties: Name, Id, StylistId(Used to track which stylist this client has an appointment for) }_
_{Methods: Save(), GetAll(), Find(int id), DeleteAll(), DeleteRow(int id), Override Equals(System.Object), GetHashCode() }_

### License

*{MIT}*

Copyright (c) 2018 **_{Alexander Neumann @ Epicodus}_**
