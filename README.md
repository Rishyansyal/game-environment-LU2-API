Requirements
Niet-functionele eisen
Unittesten: Er moeten unit tests worden opgesteld voor de secure backend om de betrouwbaarheid van de code te verbeteren. Er zijn unit tests opgesteld voor minstens drie acceptatiecriteria.
Systeemtesten: Er moeten systeemtesten worden opgesteld voor de secure backend om de betrouwbaarheid van het systeem te verbeteren.
Cloud Deploy: De secure backend moet gedeployed zijn in de cloud, zodat deze online beschikbaar is.
Version Control:
De code van de Unity applicatie moet onder versiebeheer staan op een git repository in de cloud.
De code van de secure backend moet onder versiebeheer staan op een git repository in de cloud.
Automatisering van Deployment en Testing:
Een push naar de git repository van de secure backend moet automatisch de unittests uitvoeren.
Een push naar de git repository van de secure backend moet automatisch de API en database naar de cloud deployen indien de testen slagen.
Veilige Credentials: Credentials in de cloud moeten op een veilige manier worden behandeld, zodat deze niet door personen buiten het developer team kunnen worden bekeken of gebruikt.
HTTPS Verbinding: De client applicatie moet met de API communiceren via een beveiligde HTTPS verbinding zodat niet-geautoriseerde personen of applicaties de verstuurde berichten niet kunnen lezen of aanpassen.
Bescherming tegen SQL Injection: De backend moet beschermt zijn tegen SQL injection.
Functionele eisen
De functionele eisen voor deze applicatie zijn onderverdeeld volgens de MOSCoW methode.

Must Have
Registratie:

Als gebruiker wil ik mezelf kunnen registeren op basis van een gebruikersnaam en wachtwoord.
De gebruikersnaam moet uniek zijn.
Het wachtwoord moet minimaal 10 karakters lang zijn en minstens 1 lowercase, uppercase, cijfer en niet-alphanumeriek karakter bevatten.
Inloggen:

Als gebruiker wil ik kunnen inloggen met mijn gebruikersnaam en wachtwoord.
De gebruiker krijgt een foutmelding als de gebruikersnaam of wachtwoord niet correct is.
Creëren van een 2D-wereld:

Als gebruiker wil ik een nieuwe lege 2D-wereld kunnen aanmaken.
De gebruiker moet ingelogd zijn om een wereld aan te maken.
De gebruiker moet een naam invoeren voor de nieuwe 2D-wereld.
De naam voor de nieuwe 2D-wereld mag niet identiek zijn aan de naam van een bestaande 2D-wereld van de gebruiker.
De naam moet tussen 1 en 25 karakters lang zijn.
De gebruiker mag niet meer dan 5 eigen 2D-werelden hebben.
De nieuwe 2D-wereld wordt opgeslagen.
Overzicht van bestaande 2D-werelden:

Als gebruiker wil ik een overzicht kunnen bekijken van mijn bestaande 2D-werelden.
De gebruiker moet ingelogd zijn om dit overzicht te kunnen bekijken.
Het overzicht toont de naam van de bestaande 2D-werelden van de gebruiker.
Bekijken van een bestaande 2D-wereld:

Als gebruiker wil ik één van mijn bestaande 2D-werelden kunnen bekijken.
De gebruiker moet ingelogd zijn.
2D-objecten die gekoppeld zijn aan deze 2D-wereld worden correct getoond op basis van de attributen van het 2D-object.
De gebruiker kan alleen zijn/haar eigen 2D-werelden bekijken.
Toevoegen van een 2D-object:

Als gebruiker wil ik een 2D-object aan mijn openstaande 2D-wereld kunnen toevoegen.
De gebruiker moet ingelogd zijn.
De gebruiker kan kiezen uit minimaal 3 beschikbare 2D-objecten.
Het nieuwe 2D-object wordt opgeslagen.
Verwijderen van een 2D-wereld:

Als gebruiker wil ik een door mij gemaakte 2D-wereld kunnen verwijderen.
De gebruiker moet ingelogd zijn.
2D-objecten die gekoppeld zijn aan deze 2D-wereld worden ook verwijderd.
