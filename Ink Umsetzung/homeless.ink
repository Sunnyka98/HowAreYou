== initHomeless
Nach einiger Zeit hörst du etwas über den Boden rattern. Ein Mann kommt mit einem Einkaufswagen auf dich zu.
+ Aufstehen und gehen 
    -> startAgain
+ Obdachlosen beobachten 
    -> homelessLook 
+ Wie geht es Ihnen? -> askHomeless

+ Kennst du die Mutter die hier eben vorbei kam?
    -> askHM
+ {girl >= 1} Kennst du das Mädchen, welches manchmal hier ist?
    -> askHG
+ {dogowner >= 2} Kennst du den Hundebesitzer der hier mit seinem Hund vorbei gekommen ist?
    -> askHD
+ {runner >= 4} Kennst du den Jogger, der hier vorhin durch gelaufen ist? 
    -> askHR 
    
= homelessLook
Der Mann sieht aus als wenn er draußen schlafen würde. Alles was er besitzt, tägt er entweder oder ist in seinem Wagen. Seine Kleidung sieht alt aus. 
-> homelessEnd

=askHomeless
TODO Obdachlosen frage
{ homeless <= 6: 
    Weißt du, manchmal ist diese Frage gefählicher als ein Messer in den falschen Händen.
- else:
    TODO Obdachloser öffnet sich. 
}
-> homelessEnd

= askHD
TODO Obdachlosen nach Hundebesitzer fragen 
-> homelessEnd

= askHG
TODO Obdachlosen nach Mädchen fragen 
-> homelessEnd

= askHM
TODO Obdachlosen nach Mutter fragen
~ mother = mother +1 
-> homelessEnd

= askHR
TODO Obdachlosen nach Jogger fragen
-> homelessEnd

= homelessEnd
Der Mann steht auf und sagt: "Dir noch einen schönen Tag." Danach geht er weiter mit seinem Einkaufswagen. 
-> startAgain



