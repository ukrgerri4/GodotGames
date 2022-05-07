FROM nginx:alpine

RUN rm -f /usr/share/nginx/html/*

WORKDIR /usr/share/nginx/html

COPY /Dist/html ./
RUN cp ./Tetris.html ./index.html
RUN rm ./Tetris.html
COPY /nginx/nginx.conf /etc/nginx/nginx.conf