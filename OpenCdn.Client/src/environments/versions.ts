import { VERSION } from '@angular/core';

const packageJson = require('../../package.json');

export const versions = {
    angular: VERSION.full,
    client: packageJson.version
};
