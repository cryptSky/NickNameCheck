exports.config = {
    directConnect: true,
    capabilities: {
        'browserName': 'chrome'
    },
    framework: 'jasmine',
    specs: ['../tests/e2e/*.js'],
    exclude: ['conf.js'],
    jasmineNodeOpts: {
        defaultTimeoutInterval: 9600000
    },
    baseUrl: 'https://fr8.co',
}; 