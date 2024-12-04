== initDogowner
Ein Mann mit einem Hund kommt auf dich zu. Er schaut dich an, rümpft die Nase und setzt sich auf die Bank. Er streichelt seinen Hund und lässt ihn mit dem Befehl "Los" von der Leine. 
+ Aufstehen und gehen
    ->startAgain
+ Den Mann anschauen 
    -> dogownerLook 
+ Wie geht es ihnen? 
    -> askDogowner

{days > 1: 
+ {homeless >= 4} Kennst du den Obdachlosen der manchmal hier vorbeikomm? -> askDH
+ { runner >= 2} Kennst du den Jogger der hier manchmal vorbeikommt -> askDR
+ { mother >= 3 } Kennst du die Mutter die manchmal -> askDM
}

+ { girl >= 1 } Kennst du das Mädchen, was manchmal hier vorbei kommt? -> askDG

= dogownerLook
Ein großer älterer Mann mit einem Schäferhund. In den Augen sieht man, dass er schon viel erlebt hat. 
-> dogownerEnd

= askDogowner
 TODO Den Hundebesitzer fragen 
 {dogowner <= 5: 
 "Es geht sie nichts an, wie es mir geht." 
 }
 {dogowner > 5: 
 TODO Aufrichtige Aussage 
 }
-> dogownerEnd

= askDH
TODO Hundebesitzer nach Obdachlosen fragen 
-> dogownerEnd

= askDM
TODO Hundebesitzer nach Mutter fragen 
-> dogownerEnd

= askDR
TODO Hundebesitzer nach Jogger fragen 
-> dogownerEnd

= askDG
TODO Hundebesitzer nach Mädchen fragen 
-> dogownerEnd


= dogownerEnd
Er ruft seinen Hund und geht. 
+ Du lehnst dich zurück und beobachtest die Umgebung. 
    -> initRunner
+ [Du gehst nach Hause]
    -> startAgain