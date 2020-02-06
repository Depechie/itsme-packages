from setuptools import find_packages, setup

version = '0.0.8'


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
    # Wheel distributation does not honor Manifest.in for python 3
    # we have to include binary files here
    package_data={
        'itsme': ['itsme_lib.dll', 'itsme_lib.dylib', 'itsme_lib.so'],
    },
    classifiers=[
        'Programming Language :: Python :: 3',
        'License :: OSI Approved :: MIT License',
        'Operating System :: OS Independent',
    ],
    include_package_data=True
)
