== initRunner 
Nach einiger Zeit kommt ein Mann in einem moderaten Tempo in deine Richtung. Auf deiner Höhe bleibt er stehen, geht an die unbesetzte Seite der Bank und fängt sich an zu dehnen. 
+ Aufstehen und gehen 
    -> startAgain
+ Jogger beobachten 
    -> runnerLook
+ Wie geht es Ihnen?
    -> askRunner

{ days > 0: 
    + {homeless >= 4} Kennst du den Obdachlose der hier manchmal ist? ->askRH
    + {mother >= 3} Kennst du die Mutter die hier ab und zu herkommt? -> askRM
}
+ {dogowner >= 2} Kennst du den Hundebesitzer der hier manchmal herkommt? -> askRD
+ {girl >=1 } Kennst du das Mädchen das hier manchmal hier ist? -> askRG

= runnerLook 
TODO Aussehen des Joggers
Er ist ein mittelgroßer Mann in einem sportlichen Outfit mit einem T-Shirt und einer kurzen Hose. Seine Beine zieren mehrere Narben.
-> runnerEnd

= askRunner 
{ runner <= 5: 
TODO Jogger verschlossen
- else: 
TODO Jogger öffnet sich
}
-> runnerEnd
 
= askRM
TODO Jogger nach Mutter fragen 
-> runnerEnd

= askRD
TODO Jogger nach Hundebesitzer fragen 
-> runnerEnd

= askRH
TODO Jogger nach Obdachlosen fragen 
-> runnerEnd 

= askRG
TODO Jogger nach Mädchen fragen 
-> runnerEnd 

= runnerEnd

Fertig mit dehnen dreht er sich kurz zu dir um, schenkt dir ein lächeln und joggt dann weiter. 
+  Du schaust den Kindern auf dem Spielplatz zu.
    -> initMother

+ Nach Hause gehen 
    -> startAgain
















