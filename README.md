# processing-assitant
Relativity Application: This application allows a user to create a processing set by pointing at a directory which contains folders of custodian data.  The resulting processing set will contain one processing data source for each folder in the selected directory and will associate the data source to the custodian based on the folder name.

While this is an open source project on the kCura github account, there is no support available through kCura for this solution or code. You are welcome to use the code and solution as you see fit within the confines of the license it is released under. However, if you are looking for support or modifications to the solution, we suggest reaching out to the Project Champion listed below.

# Project Champion 
![NSerio](https://kcura-media.s3.amazonaws.com/app/uploads/sites/2/2014/09/NSerio_logo.png "NSerio")

NSerio is a major contributor to this project.  If you are interested in having modifications made to this project, please reach out to [NSerio](http://nserio.com) for an estimate. 


# Project Setup
This project requires references to kCura's Relativity® SDK dlls.  These dlls are not part of the open source project and must be obtained through kCura.  In the "packages" folder under "Source" you will need to create a "Relativity" folder if one does not exist.  You will need to add the following dlls:

- kCura.Agent.dll
- kCura.EventHandler.dll
- kCura.Relativity.Client.dll
- Relativity.API.dll
- Relativity.CustomPages.dll
