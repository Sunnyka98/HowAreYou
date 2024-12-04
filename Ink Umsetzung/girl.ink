== initGirl == 
Ein Mädchen, vielleicht 14 Jahre alt, kommt auf dich zu und setzt sich neben dich. 

{days > 0:
+ {mother >= 4} Kennst du die Mutter die Manchmal hier vorbei kommt? ->askGM
+ {runner >= 3 } Kennst du den Jogger der manchmal hier vorbeikommt? ->askGR
+ {dogowner >= 2} Kennst du den Hundebesitzer der hier manchmal vorbei kommt? -> askGD
+ {homeless >=1 } Kennst du den Obdachlosen, der hier manchmal ist? -> askGH
}

+ Aufstehen und gehen
    -> startAgain
+ Das Mädchen beobachten
    -> girlLook
+ Wie geht es dir?
    ->askGirl



= girlLook
Sie wippt mit den Beinen und knetet ihre Hände, als wenn sie etwas erzählen will. Sie hat ein sommerliches Kleid an.
Nach ein paar Minuten steht sie auf und geht. 
    ->girlEnd
    
= askGirl
{ girl <= 5: 
    Sie dreht sich zu dir und schaut dich mit großen Augen an. "Spring nicht in Pfützen, bei der du den Boden nicht siehst."
}
{girl > 6: 
    TODO Sie hat genug vertrauen gefasst um dir von dem Drahtseilakt zwischen Schule und Zuhause zu erzählen. 
}
->girlEnd

= askGM
TODO Frage Mädchen nach Mutter
->girlEnd

= askGR 
TODO Frage Mädchen nach Jogger 
-> girlEnd

= askGD
TODO Frage Mädchen nach Hundebesitzer 
->girlEnd

= askGH
TODO Frage Mädchen nach Obdachlosen
-> girlEnd
    
= girlEnd
Sie steht auf und läuft davon. 
+ Die Sonne genießen 
    -> initDogowner
+ [Nach Hause gehen]
    -> startAgain
