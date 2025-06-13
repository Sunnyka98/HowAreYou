== dogowner
Nachdem du die Bank und das Mädchen hinter dir gelassen hast, spürst du die leichten Unebenheiten des Kieswegens unter deinen Füßen. Die dichten Baumkronen über dir filtern das Sonnenlicht, sodass nur wenige Strahlen den Boden erreichen. 
{days: 
- 0: ->dogownerFirstLoop
}
->DONE

== dogownerFirstLoop
!! DogOwnerFirstLoop
Plötzlich hörst du ein tiefes Bellen. Der Ton schwingt in der stillen Luft nach, gefolgt von einem rhythmischen Klacken - das Geräusch einer Hundeleine, die gegen einen Stein schlägt.
Du schaust nach vorn und siehst in der Ferne einen Mann stehen. Er hält die Leine eines Hundes in der Hand, der so eifrig schnüffelt und immer wieder aufgeregt den Kopf hebt. Der Mann hingegen wirkt still und regungslos, fast so als gehöre er nicht in diese lebendige Szene. 
+ Den Mann ansperechen 
    ->dogownerFirstLoop.positive
+ Den Mann ignorieren 
    ->dogownerFirstLoop.negative
= positive
!! DogOwner FirstLoop positive
Du entscheidest dich, auf ihn zuzugehen. "Ein beeindruckender Hund", sagst du, wäkrend du einen kurzen Blick zu dem Tier wirfst. 
Der Mann hebt den Kopf und sieht dich ruhigem, aber prüfendem Blick an. "Das ist er," antwortet er kurz, seine Stimme ruhig und kontrolliert.
Ein Moment der Stille entsteht, bevor er hinzufügt: "Er hat schon viel gesehen." Seine Hand ruht auf dem Kopf des Hundes, der aufmerksam bleibt, aber entspannt wirkt. "Wir sollten weiter," murmelt er, fast mehr zu sich selbst als zu dir. 
+ "Warum wirkt er so aufmerksam?" 
    
->DONE
= negative 
!! DogOwner FirstLoop negative
->DONE