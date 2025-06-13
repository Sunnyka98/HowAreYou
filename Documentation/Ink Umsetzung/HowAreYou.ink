INCLUDE girl.ink
INCLUDE dogowner.ink

VAR loops = 0
VAR days = 0

+ Starte Spiel
    -> loop
    
== loop

{ loops == 0: 
    Wie jeden Tag gehst du in den Park.
}
{ 
    - days == 0: 
        -> nextPerson 
    - days > 0: 
        ->choosePerson 
    - else: Oops, something went wrong. 
}

== nextPerson

{ 
    - loops == 0: 
        -> girl
    - loops == 1: 
        -> dogowner
    - else: 
        -> startAgain
}
-> DONE

== choosePerson
Du bist im Park. Wen möchtest du besuchen?

* [Das Mädchen auf der Bank] -> girl
* [Den Mann mit dem Hund] -> dogowner

== startAgain
Der Tag ist zuende und du gehst nach Hause
~ loops = 0
~ days = days + 1
-> loop

-> END
