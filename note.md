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

==========
Dry run

- Run orthanc
- build and run orthanc-adapter
  cd dicom-app/dicom-adapter
  docker build -t orthanc-adapter-dryrun:v1 .
- build and run ohif viewer
  cd dicom-app/ui/ohif-viewer
  docker build -t ohif-v3.11-custom-dryrun:v1 .
- Run Nginx
- build and run todo-api
  cd dicom-app/Todo.Api
  docker build -t todo-api-dryrun:v1 .

- build and run todo-ui
  cd dicom-app/ui/todo-ui
  docker build -t todo-ui-dryrun:v1 .
