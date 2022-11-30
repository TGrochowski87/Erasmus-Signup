interface AccessTokenRequestDto {
  oAuthToken: string;
  oAuthVerifier: string;
  oAuthTokenSecret: string;
}

export default AccessTokenRequestDto;
