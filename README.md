# Erasmus-Signup
Application that supports signing for Erasmus program.

Docker launch guide
# Frontend
  Prod:
    1. stwórz aktualnego builda aplikacji za pomocą komendy: 
       npm run build

    2. uruchom kontener wpisując polecenie: 
       docker-compose -f docker-compose-prod.yml up -d --build  
      
  Dev:
    1. uruchom kontener wpisując polecenie: 
       docker-compose -f docker-compose-dev.yml up -d --build
     
     flag -d --build należy używać w przypadku braku obrazu (tj. pierwsze uruchomienie kontenera lub po przeczyszczeniu pamięci dockera z obrazów (ang. images))
      