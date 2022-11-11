# Erasmus-Signup
Application that supports signing for Erasmus program.

Docker launch guide
# Frontend
  Prod:
     docker-compose -f docker-compose-prod.yml up -d --build  
      (flaga -d --build tylko gdy 1 raz budujemy = nie mamy obrazu kontenera)
  Dev:
     docker-compose -f docker-compose-dev.yml up -d --build
      (flaga -d --build tylko gdy 1 raz budujemy = nie mamy obrazu kontenera)
      