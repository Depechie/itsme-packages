from os import name as operating_system
from setuptools import find_packages, setup
from subprocess import call

# Download ITSME libraries
lib_version = '0.5.0.1570598667'
version = '0.0.6'
location = './itsme'

if operating_system == 'nt':
    call(f'../scripts/dependencies.ps1 -libVersion {lib_version} -location {location}', shell=True)
else:
    call(f'../scripts/dependencies.sh {lib_version} {location}', shell=True)

setup(
    name='itsme',
    version=version,
    author='itsme_sdk',
    author_email='itsme.store@inthepocket.mobi',
    description='A package to integrate with the itsme OIDC API',
    long_description='This is a library to integrate with the ITSME backend',
    long_description_content_type='text/markdown',
    url='https://github.com/itsme-api/itsme-api-python',
    packages=find_packages(),
    classifiers=[
        'Programming Language :: Python :: 3',
        'License :: OSI Approved :: MIT License',
        'Operating System :: OS Independent',
    ],
    include_package_data=True
)
