window.config = {
  routerBasename: '/',

  showStudyList: true,

  extensions: [
    '@ohif/extension-default',
    '@ohif/extension-cornerstone',
  ],

  modes: [
    '@ohif/mode-longitudinal',
  ],

  dataSources: [
    {
      namespace: '@ohif/extension-default.dataSourcesModule.dicomweb',
      sourceName: 'orthanc',
      configuration: {
        name: 'Orthanc',

        qidoRoot: 'http://localhost/orthanc/dicom-web',
        wadoRoot: 'http://localhost/orthanc/dicom-web',

        wadoUriRoot: null,

        qidoSupportsIncludeField: true,
        supportsReject: false,

        supportsWadoUri: false,
        supportsWadoRs: true,

        imageRendering: 'wadors',
        thumbnailRendering: 'wadors',
      },
    },
  ],

  defaultDataSourceName: 'orthanc'
};