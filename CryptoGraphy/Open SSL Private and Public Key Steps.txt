----With Password-----------

openssl genpkey -algorithm RSA -out private_key.pem -aes256
Enter PEM pass phrase:
Verifying - Enter PEM pass phrase:

openssl rsa -in private_key.pem -outform PEM -pubout -out public_key.pem
Enter pass phrase for private_key.pem:
writing RSA key


----Without Password-----------

openssl genpkey -algorithm RSA -out private_key.pem
openssl rsa -in private_key.pem -outform PEM -pubout -out public_key.pem
writing RSA key
