type Token = {

    accessToken: string;

    accessTokenExpiresIn: number;

    refreshToken: string;

    refreshTokenExpiresIn: Date;

    tokenType: string;
};

export default Token;