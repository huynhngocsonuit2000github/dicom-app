- Build custom OHIF
  docker build -t ohif-v3.11-custom:v2 .
  - v1: simple ohif with custom button
  - v2: button call adapter

- Build Orthanc Adapter
  docker build -t orthanc-adapter:v2 .
  - v1: simple adapter
  - v2: find by study id

- OHIF
  - Deploy: http://localhost/orthanc/dicom-web
  - Dev:

        wadoUriRoot: 'https://dd14fa38qiwhyfd.cloudfront.net/dicomweb',
        wadoUriRoot: 'http://localhost/orthanc/dicom-web',
